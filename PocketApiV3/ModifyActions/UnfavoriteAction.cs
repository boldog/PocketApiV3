using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketApiV3.ModifyActions
{
    /// <summary>
    /// Remove an item from the user's favorites.
    /// </summary>
    public sealed class UnfavoriteAction : ItemAction
    {
        internal override string ActionApiVerb => "unfavorite";
    }
}
