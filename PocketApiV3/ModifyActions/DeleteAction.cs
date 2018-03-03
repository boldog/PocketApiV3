using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketApiV3.ModifyActions
{
    /// <summary>
    /// Permanently remove an item from the user's account.
    /// </summary>
    public sealed class DeleteAction : ItemAction
    {
        internal override string ActionApiVerb => "delete";
    }
}
