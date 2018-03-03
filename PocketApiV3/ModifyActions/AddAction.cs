using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace PocketApiV3.ModifyActions
{
    public abstract class AddAction : Action
    {
        /// <summary>
        /// Optional.  A Twitter status id; this is used to show tweet attribution.
        /// </summary>
        [JsonProperty("ref_id")]
        public long? TweetId;

        /// <summary>
        /// Optional.  A comma-delimited list of one or more tags.
        /// </summary>
        [JsonProperty("tags")]
        public string Tags { get; set; }

        /// <summary>
        /// Optional.  The title of the item.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        public void SetTags(IEnumerable<string> tags) =>
            Tags = TagHelper.Join(tags);


        protected AddAction() { }

        internal override string ActionApiVerb => "add";

    }
}
