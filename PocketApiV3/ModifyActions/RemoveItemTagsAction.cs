using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketApiV3.ModifyActions
{
    /// <summary>
    /// Remove one or more tags from an item.
    /// </summary>
    public sealed class RemoveItemTagsAction : ItemTagsAction
    {
        internal override string ActionApiVerb => "tags_remove";
    }
}
