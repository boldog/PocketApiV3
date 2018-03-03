using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace PocketApiV3.ModifyActions
{
    public abstract class ItemAction : Action
    {
        /// <summary>
        /// The id of the item to perform the action on.
        /// </summary>
        [JsonProperty("item_id")]
        public long ItemId { get; set; }
    }
}
