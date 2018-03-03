using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace PocketApiV3.ModifyActions
{
    public abstract class ItemTagsAction : ItemAction
    {
        [JsonIgnore]
        public string[] Tags { get; set; }

        /// <summary>
        /// A comma-delimited list of one or more tags to remove.
        /// </summary>
        [JsonProperty("tags")]
        public string TagsDelimited
        {
            get => TagHelper.Join(Tags);
            set => Tags = TagHelper.Split(value);
        }

        protected ItemTagsAction() { }

    }
}
