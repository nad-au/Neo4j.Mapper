using System;
using System.Linq;
using System.Threading.Tasks;
using IntegrationTests.Models;
using Neo4jMapper;
using NUnit.Framework;

namespace IntegrationTests.Tests
{
    [TestFixture]
    [NonParallelizable]
    public class TemporalTests : TestFixtureBase
    {
        [SetUp]
        protected override async Task SetUp()
        {
            await base.SetUp();
            await Session.RunAsync("CREATE (t:TimeStamp {name: 'Worker', when: dateTime()})");
        }

        [TearDown]
        protected async Task TearDown()
        {
            await Session.RunAsync("MATCH (t:TimeStamp) DELETE (t)");
        }

        [Test]
        public async Task Should_Map_Temporal_Value()
        {
            var now = DateTime.UtcNow;

            var result = await Session.RunAsync(@"
                MATCH (timestamp:TimeStamp)
                RETURN timestamp");

            var timeStamps = await result.MapAsync<TimeStamp>();

            Assert.AreEqual(1, timeStamps.Count);

            var timeStamp = timeStamps.Single();

            Assert.AreEqual(now.Year, timeStamp.When.Year);
            Assert.AreEqual(now.Month, timeStamp.When.Month);
            Assert.AreEqual(now.Day, timeStamp.When.Day);
            Assert.AreEqual(now.Hour, timeStamp.When.Hour);
            Assert.AreEqual(now.Minute, timeStamp.When.Minute);
        }
    }
}
