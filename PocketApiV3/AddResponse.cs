using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace PocketApiV3
{
    public sealed class AddResponse : Response
    {
        [JsonProperty("item")]
        public AddResponseItem Item { get; set; }
    }
}
