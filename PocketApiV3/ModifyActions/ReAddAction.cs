using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketApiV3.ModifyActions
{
    /// <summary>
    /// Move an item from the user's archive back into their unread list.
    /// </summary>
    public sealed class ReAddAction : ItemAction
    {
        internal override string ActionApiVerb => "readd";
    }
}
