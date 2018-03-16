using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PocketApiV3;
using Xunit;

namespace Tests
{
    public class JsonFileTests
    {

        [Fact]
        public void CanDeserializeRetrieveRequestJsonFile()
        {
            var json = File.ReadAllText("SampleData\\RetrieveResponse.json");

            var retrieveResponse = PocketApiV3.ApiSerializer.Instance.Deserialize<RetrieveResponse>(json);


            Assert.True(true);

        }

    }
}
