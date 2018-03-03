using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace PocketApiV3
{
    public class StatisticsResponse : Response
    {
        [JsonProperty("count_list")]
        public int CountAll { get; set; }

        [JsonProperty("count_read")]
        public int CountRead { get; set; }

        [JsonProperty("count_unread")]
        public int CountUnread { get; set; }
    }
}
