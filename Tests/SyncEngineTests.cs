using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PocketApiV3.Persistence;
using Xunit;
using Xunit.Abstractions;

namespace Tests
{
    public class SyncEngineTests : PocketClientTestClass
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

        [Fact]
        public async Task SyncEngineSyncsWithoutErrors()
        {
            AssertPocketClientIsValid();

            using (var syncEngine = new SyncEngine(_pocketClient, GetDefaultSyncEngineSettings()))
            {
                await syncEngine.Initialize(default);
                await syncEngine.SynchronizeAsync(default);

                var stats = await syncEngine.GetStatistics(default);
                Assert.NotEqual(0, stats.CountAll);
            }
        }


        SyncEngineSettings GetDefaultSyncEngineSettings()
        {
            var builder = new SyncEngineSettings.Builder()
            {

            };
            return builder.Build();
        }

    }
}
