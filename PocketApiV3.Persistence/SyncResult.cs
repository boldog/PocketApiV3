using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketApiV3.Persistence
{
    public class SyncResult
    {
        SyncResult(Builder builder)
        {
            Id = builder.Id;
            StartTime = builder.StartTime;
            EndTime = builder.EndTime ?? DateTimeOffset.UtcNow;
            Code = builder.Code;

        }

        public Guid Id { get; }
        public DateTimeOffset StartTime { get; }
        public DateTimeOffset EndTime { get; }
        public SyncResultCode Code { get; }


        internal class Builder
        {
            public Builder(Guid? id = null, DateTimeOffset? startTime = null)
            {
                Id = id ?? Guid.NewGuid();
                StartTime = startTime ?? DateTimeOffset.UtcNow;
            }

            public Guid Id { get; set; }
            public DateTimeOffset StartTime { get; set; }
            public DateTimeOffset? EndTime { get; set; }
            public SyncResultCode Code { get; set; }


            public SyncResult Build() => new SyncResult(this);
        }

    }
}
