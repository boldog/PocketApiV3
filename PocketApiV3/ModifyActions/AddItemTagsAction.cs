using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketApiV3.ModifyActions
{
    /// <summary>
    /// Add one or more tags to an item.
    /// </summary>
    public sealed class AddItemTagsAction : ItemTagsAction
    {
        internal override string ActionApiVerb => "tags_add";
    }
}
