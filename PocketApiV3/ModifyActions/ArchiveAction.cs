using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketApiV3.ModifyActions
{
    /// <summary>
    /// Move an item to the user's archive.
    /// </summary>
    public sealed class ArchiveAction : ItemAction
    {
        internal override string ActionApiVerb => "archive";
    }
}
