using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketApiV3.Persistence
{
    public class SyncResults
    {
        SyncResults(Builder builder)
        {
            ResultCode = builder.ResultCode;
            TimeSyncStarted = builder.TimeSyncStarted;
            TimeSyncEnded = builder.TimeSyncEnded;
        }

        public SyncResultCode ResultCode { get; }

        public DateTimeOffset TimeSyncStarted { get; }
        public DateTimeOffset TimeSyncEnded { get; }


        class Builder
        {
            public SyncResultCode ResultCode { get; set; }
            public DateTimeOffset TimeSyncStarted { get; set; }
            public DateTimeOffset TimeSyncEnded { get; set; }

            public SyncResults Build() => new SyncResults(this);
        }
    }
}
