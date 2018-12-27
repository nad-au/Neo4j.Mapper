using System;
using System.Linq;
using IntegrationTests.Models;
using Neo4j.Driver.V1;
using Neo4jMapper;
using NUnit.Framework;

namespace IntegrationTests.V35Tests
{
    [TestFixture]
    public class TemporalTests : TestFixtureBase
    {
        private ISession session;

        [SetUp]
        public void SetUp()
        {
            session = Driver.Session();
            session.Run("CREATE (t:TimeStamp {name: 'Worker', when: dateTime()})");
        }

        [TearDown]
        public void TearDown()
        {
            session.Run("MATCH (t:TimeStamp) DELETE (t)");
            session.Dispose();
        }

        [Test]
        public void DateTest()
        {
            var result = session.Run(@"
                MATCH (timestamp:TimeStamp)
                RETURN timestamp
                LIMIT 10");

            var timeStamps = result.Return<TimeStamp>().ToList();

            Assert.AreEqual(1, timeStamps.Count);

            var timeStamp = timeStamps.Single();

            var now = DateTime.UtcNow;
            Assert.AreEqual(now.Year, timeStamp.When.Year);
            Assert.AreEqual(now.Month, timeStamp.When.Month);
            Assert.AreEqual(now.Day, timeStamp.When.Day);
            Assert.AreEqual(now.Hour, timeStamp.When.Hour);
            Assert.AreEqual(now.Minute, timeStamp.When.Minute);
        }
    }
}
