using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PocketApiV3.Persistence;
using Xunit;
using Xunit.Abstractions;

namespace Tests
{
    public class SyncEngineTests : TestClass
    {
        public SyncEngineTests(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
        }


        [Fact]
        public void SyncEngineSettingsBuildCorrectly()
        {
            var syncEngineSettingsBuilder = new SyncEngineSettings.Builder()
            {
                MinimumTimeBetweenSynchronizationAttempts = BogusDates.Timespan()
            };

            var syncEngineSettings = syncEngineSettingsBuilder.Build();

            Assert.Equal(syncEngineSettingsBuilder.MinimumTimeBetweenSynchronizationAttempts, syncEngineSettings.MinimumTimeBetweenSynchronizationAttempts);
        }

    }
}
