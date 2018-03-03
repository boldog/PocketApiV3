using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketApiV3.ModifyActions
{
    /// <summary>
    /// Replace all of the tags for an item with the one or more provided tags.
    /// </summary>
    public sealed class ReplaceItemTagsAction : ItemTagsAction
    {
        internal override string ActionApiVerb => "tags_replace";
    }
}
