using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace PocketApiV3
{
    public abstract class Response
    {
        protected Response() { }

        /// <summary>
        /// Indicates whether this <see cref="Response"/> is a good, non-error
        /// response.
        /// </summary>
        [JsonProperty("status")]
        public bool IsStatusOK { get; set; }

        [JsonIgnore]
        public string RawJson { get; set; }
    }
}
