using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace PocketApiV3.ModifyActions
{
    /// <summary>
    /// Add a new item to the user's list.  If you're doing it by ID, it must
    /// already be added, right?  So does this undelete an item?
    /// </summary>
    public sealed class AddItemById : AddAction
    {
        /// <summary>
        /// The id of the item to perform the action on.
        /// </summary>
        [JsonProperty("item_id")]
        public long ItemId { get; set; }
    }
}
