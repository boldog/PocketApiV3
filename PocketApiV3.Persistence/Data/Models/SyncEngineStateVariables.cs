using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace PocketApiV3.Persistence.Data.Models
{
    public class SyncEngineStateVariables
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int Id { get; set; }

        //public DateTime TimeLastSynchronized

        public DateTimeOffset? LastSyncStartTime { get; set; }
    }
}
