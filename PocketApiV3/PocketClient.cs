using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace PocketApiV3
{
    public sealed class PocketClient : IPocketClient
    {
        public PocketClient(string consumerKey)
            : this(consumerKey, null) { }

        public PocketClient(string consumerKey, string accessToken)
        {
            ConsumerKey = consumerKey;
            AccessToken = accessToken;

            _httpClient = new HttpClient()
            {
                BaseAddress = BasePocketUrl
            };

            // Pocket needs this specific Accept header :-S
            _httpClient.DefaultRequestHeaders.Add("Accept", "*/*");

            // Defines the response format (according to the Pocket docs)
            _httpClient.DefaultRequestHeaders.Add("X-Accept", "application/json");
        }

        public string ConsumerKey { get; }

        public string AccessToken { get; private set; }

        public bool CacheHttpResponseData { get; set; } = true;

        /// <summary>
        /// Pocket has a mobile and desktop version of the authorization page.
        /// Theirs servers will try to detect what type of device the user is
        /// using and redirect accordingly. In cases where we do not detect
        /// screen size (such as on Windows Phone), you can set this to true
        /// to force the mobile version.
        /// </summary>
        public bool? IsMobileClient { get; set; }

        public PocketLimits Limits { get; set; }

        public TimeSpan Timeout
        {
            get => _httpClient.Timeout;
            set => _httpClient.Timeout = value;
        }
        public bool? UseInsideWebAuthenticationBroker { get; set; }

        public void Dispose()
        {
            if (_httpClient != null)
            {
                _httpClient.Dispose();
                _httpClient = null;
            }
        }

        public async Task<TResponse> SendRequest<TResponse>(Request<TResponse> request, CancellationToken cancellationToken = default)
            where TResponse : Response
        {
            //// rewrite base if it is a request to the Parser API
            //if (isReaderRequest && _restParserClient == null)
            //    throw new PocketException("Please pass the parserUri in the PocketClient ctor.");

            string redirectUri = null;

            // Interesting - pattern matching is the only way to get request as the object we want.
            switch (request)
            {
                case AuthenticateStep1Request authStep1Request:
                    redirectUri = authStep1Request.RedirectUri;
                    break;
            }

            string json = await SendRequestCore(request, cancellationToken);
            var result = ApiSerializer.Instance.Deserialize<TResponse>(json);
            result.RawJson = json;

            switch (result)
            {
                case AuthenticateStep1Response authStep1Response:
                    authStep1Response.AuthenticationUri = GetAuthenticationUrl(authStep1Response.RequestCode, redirectUri);
                    break;

                case AuthenticateStep2Response authStep2Response:
                    // Scoop the AccessToken outta there.  Blammo.
                    AccessToken = authStep2Response.AccessToken;
                    break;
            }

            return result;
        }

        #region Non-Public Members

        async Task<string> SendRequestCore(Request request, CancellationToken cancellationToken)
        {
            string responseString = null;

            Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage)
            {
                //if (isReaderRequest)
                //    return _restParserClient.SendAsync(httpRequestMessage, cancellationToken);
                //else
                return _httpClient.SendAsync(httpRequestMessage, cancellationToken);
            }

            // every single Pocket API endpoint requires HTTP POST data
            using (var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, request.ApiMethod))
            {
                httpRequestMessage.Content = GetRequestContent(request);

                try
                {
                    using (var response = await SendAsync(httpRequestMessage).ConfigureAwait(false))
                    {
                        ValidateResponse(response);

                        var limits = Limits;
                        if (limits != null)
                            ExtractLimitsFromHeader(limits, response.Headers);

                        responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                        if (CacheHttpResponseData)
                        {
                            _lastResponseHeaders = response.Headers;
                            _lastResponseData = responseString;
                        }
                    }
                }
                catch (HttpRequestException ex)
                {
                    throw new PocketException(ex.Message, ex);
                }
            }

            // Replace any empty arrays with an empty object.  For some reason, the Pocket v3
            // API properties that are collections, like [Pocket Item].[Authors] will return an empty array
            // if it's empty, otherwise a Dictionary-like object.  <grumble>
            var json = responseString.Replace("[]", "{}");
            return json;
        }

        static void ExtractLimitsFromHeader(PocketLimits limits, HttpResponseHeaders headers)
        {
            bool TryGetHeaderInt(string key, out int intValue)
            {
                intValue = 0;
                return
                    headers.TryGetValues(key, out IEnumerable<string> headerValues)
                    && int.TryParse(headerValues.FirstOrDefault(), out intValue);
            }

            var now = DateTimeOffset.UtcNow;

            int value;
            //if (headers.TryGetValues("X-Limit-Key-Limit", out values))
            if (TryGetHeaderInt("X-Limit-Key-Limit", out value))
                limits.ConsumerKeyRateLimit = value;
            if (TryGetHeaderInt("X-Limit-Key-Remaining", out value))
                limits.ConsumerKeyCallsRemaining = value;
            if (TryGetHeaderInt("X-Limit-Key-Reset", out value))
                limits.ConsumerKeyLimitResets = now + TimeSpan.FromSeconds(value);
            if (TryGetHeaderInt("X-Limit-User-Limit", out value))
                limits.UserRateLimit = value;
            if (TryGetHeaderInt("X-Limit-User-Remaining", out value))
                limits.UserCallsRemaining = value;
            if (TryGetHeaderInt("X-Limit-User-Reset", out value))
                limits.UserLimitResets = now + TimeSpan.FromSeconds(value);
        }

        static readonly Uri BasePocketUrl = new Uri("https://getpocket.com/v3/");
        const string AuthenticationUriBase = "https://getpocket.com/auth/authorize?";

        HttpClient _httpClient;

        HttpResponseHeaders _lastResponseHeaders;
        string _lastResponseData;

        string GetAuthenticationUrl(string requestCode, string callbackUri)
        {
            IEnumerable<string> GetQueryParts()
            {
                yield return "request_token=" + requestCode;

                yield return "redirect_uri=" + Uri.EscapeDataString(callbackUri);

                if (IsMobileClient != null)
                    yield return "mobile=" + (IsMobileClient.Value ? "1" : "0");

                // Not documented by Pocket, but saw in PocketSharp do this
                //yield return "force=login";

                // Not documented by Pocket, but saw in PocketSharp do this
                if (UseInsideWebAuthenticationBroker != null)
                    yield return "webauthenticationbroker=" + (UseInsideWebAuthenticationBroker.Value ? "1" : "0");
            }

            var result = AuthenticationUriBase + string.Join("&", GetQueryParts());
            return result;
        }

        HttpContent GetRequestContent(Request request)
        {
            var jObj = ApiSerializer.Instance.SerializeToJObject(request);

            if (request.ApiAuthRequired)
            {
                if (AccessToken == null)
                    throw new PocketClientException($"This type of request ({request.GetType().Name} requires authentication, but this client does not have an access token set.  Either send the proper authorization requests, or set the AccessToken property.");

                jObj["access_token"] = AccessToken;
            }

            jObj["consumer_key"] = ConsumerKey;

            switch (request.ApiRequestMode)
            {
                case ApiRequestContentType.FormUrlEncoded:
                    {
                        var jObjMapped = ((IEnumerable<KeyValuePair<string, JToken>>)jObj)
                            .Select(kvp => new KeyValuePair<string, string>(kvp.Key, kvp.Value.ToString()));

                        return new FormUrlEncodedContent(jObjMapped);
                    }

                case ApiRequestContentType.JsonBody:
                    return new StringContent(jObj.ToString(), System.Text.Encoding.UTF8, "application/json");

                default:
                    throw new ArgumentOutOfRangeException($"Unexpected {nameof(ApiRequestContentType)} value \"{request.ApiRequestMode}\"");
            }
        }

        string GetHeaderValueOrDefault(HttpResponseHeaders headers, string key)
        {
            if (headers != null && !string.IsNullOrEmpty(key) &&
                headers.TryGetValues(key, out IEnumerable<string> values))
            {
                return values.First();
            }
            else
            {
                return null;
            }
        }

        void ValidateResponse(HttpResponseMessage response)
        {
            // no error found
            if (response.StatusCode == HttpStatusCode.OK)
                return;

            //string exceptionString = response.ReasonPhrase;

            // fetch custom pocket headers
            var pocketErrorMessage = GetHeaderValueOrDefault(response.Headers, "X-Error");

            if (pocketErrorMessage != null)
            {
                int pocketErrorCode = Convert.ToInt32(GetHeaderValueOrDefault(response.Headers, "X-Error-Code"));
                string exceptionString = $"Pocket error: {pocketErrorMessage} ({pocketErrorCode})";
                var ex = new PocketException(exceptionString, pocketErrorCode, null);
                ex.Data.Add("X-Error", pocketErrorMessage);
                ex.Data.Add("X-Error-Code", pocketErrorCode);
                throw ex;
            }
            else
            {
                string message = $"Request error: {response.ReasonPhrase} ({(int)response.StatusCode})";
                throw new PocketException(message);
            }
        }

        #endregion
    }
}
