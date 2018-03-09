using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketApiV3.Persistence
{
    public enum SyncResultCode
    {
        Undefined = 0,
        Completed,
        SyncAlreadyInProgress,
        MinimumTimeSinceLastSyncNotReached,
    }
}
