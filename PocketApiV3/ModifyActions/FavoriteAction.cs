using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketApiV3.ModifyActions
{
    /// <summary>
    /// Mark an item as a favorite.
    /// </summary>
    public sealed class FavoriteAction : ItemAction
    {
        internal override string ActionApiVerb => "favorite";
    }
}
