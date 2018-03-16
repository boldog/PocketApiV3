using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace PocketApiV3
{
    public class PocketAuthor : IPocketAuthorData
    {
        [JsonProperty("item_id")]
        public long? ItemId { get; set; }

        // TODO see if this can be made non-nullable
        [JsonProperty("author_id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
