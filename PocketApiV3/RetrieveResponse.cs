using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace PocketApiV3
{
    public sealed class RetrieveResponse : Response
    {
        [JsonProperty("complete")]
        public bool IsComplete { get; set; }

        [JsonProperty("list")]
        public Dictionary<long, RetrieveResponseItem> Items { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

        // ignoring the search_meta property, as it is not mentioned in documentation

        [JsonProperty("since", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTimeOffset Since { get; set; }
    }
}
