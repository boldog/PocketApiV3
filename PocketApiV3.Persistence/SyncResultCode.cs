using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketApiV3.Persistence
{
    public enum SyncResultCode
    {
        Completed = 0,
        MinimumTimeSinceLastSyncNotReached,
    }
}
