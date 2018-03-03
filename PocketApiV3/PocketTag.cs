using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace PocketApiV3
{
    public class PocketTag
    {
        [JsonProperty("item_id")]
        public long? ItemId { get; set; }

        [JsonProperty("tag")]
        public string Name { get; set; }
    }

}
