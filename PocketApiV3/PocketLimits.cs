using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace PocketApiV3
{
    public class PocketLimits
    {
        /// <summary>
        /// Current rate limit enforced per consumer key.
        /// </summary>
        //[JsonProperty("X-Limit-Key-Limit")]
        public int ConsumerKeyRateLimit { get; set; }

        /// <summary>
        /// Number of calls remaining before hitting consumer key's rate limit.
        /// </summary>
        //[JsonProperty("X-Limit-Key-Remaining")]
        public int ConsumerKeyCallsRemaining { get; set; }

        /// <summary>
        /// Time when consumer key rate limit resets
        /// </summary>
        //[JsonProperty("X-Limit-Key-Reset")]
        public DateTimeOffset ConsumerKeyLimitResets { get; set; }

        /// <summary>
        /// Current rate limit enforced per user.
        /// </summary>
        //[JsonProperty("X-Limit-User-Limit")]
        public int UserRateLimit { get; set; }

        /// <summary>
        /// Number of calls remaining before hitting user's rate limit.
        /// </summary>
        //[JsonProperty("X-Limit-User-Remaining")]
        public int UserCallsRemaining { get; set; }

        /// <summary>
        /// Time when user's rate limit resets.
        /// </summary>
        //[JsonProperty("X-Limit-User-Reset")]
        public DateTimeOffset UserLimitResets { get; set; }

        [JsonIgnore]
        public int EffectiveCallsRemaining => Math.Min(ConsumerKeyCallsRemaining, UserCallsRemaining);
    }

}
