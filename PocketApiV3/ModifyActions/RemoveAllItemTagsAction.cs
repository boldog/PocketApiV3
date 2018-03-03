using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketApiV3.ModifyActions
{
    /// <summary>
    /// Remove all tags from an item.
    /// </summary>
    public sealed class RemoveAllItemTagsAction : ItemAction
    {
        internal override string ActionApiVerb => "tags_clear";
    }
}
