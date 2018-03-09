using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using PocketApiV3;
using Xunit;
using Xunit.Abstractions;

namespace Tests
{
    public abstract class PocketClientTestClass : TestClass, IDisposable
    {
        protected IPocketClient _pocketClient;

        public PocketClientTestClass(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
            var consumerKey = Environment.GetEnvironmentVariable("POCKET_CONSUMER_KEY");
            var accessToken = Environment.GetEnvironmentVariable("POCKET_ACCESS_TOKEN");
            _pocketClient = new PocketClient(consumerKey, accessToken);
        }

        public void AssertPocketClientIsValid()
        {
            Assert.NotNull(_pocketClient.ConsumerKey);
            Assert.NotEqual(0, _pocketClient.ConsumerKey.Length);
            Assert.NotNull(_pocketClient.AccessToken);
            Assert.NotEqual(0, _pocketClient.AccessToken.Length);
        }

        public void Dispose() => Dispose(true);

        protected virtual void Dispose(bool isDisposing)
        {
            Interlocked.Exchange(ref _pocketClient, null)?.Dispose();
        }
    }
}
