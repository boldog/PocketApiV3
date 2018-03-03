using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace PocketApiV3.ModifyActions
{
    /// <summary>
    /// Add a new item to the user's list.
    /// </summary>
    public sealed class AddItemByUrl : AddAction
    {
        public AddItemByUrl() { }
        public AddItemByUrl(string url)
        {
            Url = url;
        }

        /// <summary>
        /// The url of the item.
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
