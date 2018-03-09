using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PocketApiV3
{
    public sealed class AuthenticateStep1Request : Request<AuthenticateStep1Response>
    {
        /// <summary>
        /// The URL to be called when the authorization process has been
        /// completed. This URL should direct back to your application.
        /// See the Pocket API docs, Platform Specific Notes section for
        /// details about setting up custom urls for the redirect_uri on
        /// iOS and Android.
        /// </summary>
        [JsonProperty("redirect_uri")]
        public string RedirectUri { get; set; }

        /// <summary>
        /// Optional.  A string of metadata used by your app. This string will
        /// be returned in all subsequent authentication responses.
        /// </summary>
        [JsonProperty("state")]
        public string State { get; set; }


        internal override bool ApiAuthRequired => false;

        internal override string ApiMethod => "oauth/request";

        internal override ApiRequestContentType ApiRequestMode => ApiRequestContentType.FormUrlEncoded;
    }
}
