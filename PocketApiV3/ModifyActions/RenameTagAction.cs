using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace PocketApiV3.ModifyActions
{
    public sealed class RenameTagAction : Action
    {
        /// <summary>
        /// The tag name that will be replaced.
        /// </summary>
        [JsonProperty("old_tag")]
        public string OldTag { get; set; }

        /// <summary>
        /// The new tag name that will be added.
        /// </summary>
        [JsonProperty("new_tag")]
        public string NewTag { get; set; }

        internal override string ActionApiVerb => "tag_rename";
    }
}
