using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace PocketApiV3
{
    public sealed class AuthenticateStep2Request : Request<AuthenticateStep2Response>
    {
        internal AuthenticateStep2Request() { }

        public AuthenticateStep2Request(AuthenticateStep1Response authStep1Response)
        {
            RequestCode = authStep1Response.RequestCode;
        }

        /// <summary>
        /// The request token supplied in the code field of the /v3/oauth/request call.
        /// This can be gotten from the AuthorizeStep1Response.RequestCode property.
        /// </summary>
        [JsonProperty("code")]
        public string RequestCode { get; set; }

        internal override bool ApiAuthRequired => false;

        internal override string ApiMethod => "oauth/authorize";

        internal override ApiRequestContentType ApiRequestMode => ApiRequestContentType.JsonBody;

    }
}
