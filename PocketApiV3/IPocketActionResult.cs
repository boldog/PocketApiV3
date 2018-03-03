using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace PocketApiV3
{
    public interface IPocketActionResult
    {
        bool Success { get; }
    }

    public class AddActionResult : IPocketActionResult
    {
        [JsonIgnore]
        public bool Success => true;
    }

    public class BooleanActionResult : IPocketActionResult
    {
        public bool Success { get; set; }
    }
}
