using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace PocketApiV3
{
    public class ModifyResponse : Response
    {
        [JsonProperty("action_results")]
        public List<IPocketActionResult> ActionResults { get; set; }
    }
}
