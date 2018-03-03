using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace PocketApiV3
{
    public abstract class Request
    {
        protected Request() { }

        [JsonIgnore]
        internal abstract bool ApiAuthRequired { get; }

        [JsonIgnore]
        internal abstract string ApiMethod { get; }

        [JsonIgnore]
        internal abstract ApiRequestContentType ApiRequestMode { get; }
    }
}
