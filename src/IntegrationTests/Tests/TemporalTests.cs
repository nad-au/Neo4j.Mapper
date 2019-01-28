using System;
using System.Linq;
using IntegrationTests.Models;
using Neo4j.Driver.V1;
using Neo4jMapper;
using NUnit.Framework;

namespace IntegrationTests.Tests
{
    [TestFixture]
    public class TemporalTests : TestFixtureBase
    {
        protected ISession Session;

        [SetUp]
        public void SetUp()
        {
            Session = Driver.Session();
            Session.Run("CREATE (t:TimeStamp {name: 'Worker', when: dateTime()})");
        }

        [TearDown]
        public void TearDown()
        {
            Session.Run("MATCH (t:TimeStamp) DELETE (t)");
            Session.Dispose();
        }

        [Test]
        public void Should_Map_Temporal_Value()
        {
            var now = DateTime.UtcNow;

            var result = Session.Run(@"
                MATCH (timestamp:TimeStamp)
                RETURN timestamp");

            var timeStamps = result.Map<TimeStamp>().ToList();

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
