using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketApiV3.Persistence
{
    public class SyncEngineSettings
    {
        SyncEngineSettings(Builder builder)
        {
            MinimumTimeBetweenSynchronizationAttempts = builder.MinimumTimeBetweenSynchronizationAttempts;
        }

        public TimeSpan MinimumTimeBetweenSynchronizationAttempts { get; }

        public class Builder
        {
            public TimeSpan MinimumTimeBetweenSynchronizationAttempts { get; set; }

            public SyncEngineSettings Build() =>
                new SyncEngineSettings(this);
        }
    }
}
