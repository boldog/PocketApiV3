using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace PocketApiV3
{
    public class RetrieveRequest : Request<RetrieveResponse>
    {
        [JsonProperty("state")]
        public RetrieveState? State { get; set; }

        [JsonProperty("favorite")]
        public bool? IsFavorite { get; set; }

        [JsonProperty("tag")]
        public string Tag { get; set; }

        [JsonProperty("contentType")]
        public RetrieveContentType? ContentType { get; set; }

        [JsonProperty("sort")]
        public RetrieveSort? Sort { get; set; }

        [JsonProperty("detailType")]
        public RetrieveDetailType? DetailType { get; set; }

        [JsonProperty("search")]
        public string Search { get; set; }

        [JsonProperty("domain")]
        public string Domain { get; set; }

        [JsonProperty("since")]
        public DateTimeOffset? Since { get; set; }

        [JsonProperty("count")]
        public int? Count { get; set; }

        [JsonProperty("offset")]
        public int? Offset { get; set; }


        internal override bool ApiAuthRequired => true;
        internal override string ApiMethod => "get";
        internal override ApiRequestContentType ApiRequestMode => ApiRequestContentType.JsonBody;

    }
}
