using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace PocketApiV3.ModifyActions
{
    public abstract class Action
    {
        [JsonProperty("action")]
        internal abstract string ActionApiVerb { get; }

        /// <summary>
        /// Optional.  The time the action occurred.
        /// </summary>
        [JsonProperty("time")]
        public DateTimeOffset? Timestamp { get; set; }

        #region Non-Public Members

        protected Action() { }

        #endregion
    }
}
