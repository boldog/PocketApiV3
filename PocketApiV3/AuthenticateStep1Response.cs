using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace PocketApiV3
{
    public class AuthenticateStep1Response : Response
    {
        internal AuthenticateStep1Response() { }

        /// <summary>
        /// Contains a valid URL to redirect the user to for authentication.
        /// </summary>
        [JsonIgnore]
        public string AuthenticationUri { get; set; }

        [JsonProperty("code")]
        public string RequestCode { get; set; }
    }
}
