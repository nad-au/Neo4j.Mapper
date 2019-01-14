using System.Linq;
using IntegrationTests.Models;
using Neo4j.Driver.V1;
using Neo4jMapper;
using NUnit.Framework;

namespace IntegrationTests.Tests
{
    [TestFixture]
    public class ParametersTests : TestFixtureBase
    {
        protected ISession Session;

        [SetUp]
        public void SetUp()
        {
            Session = Driver.Session();
        }

        [TearDown]
        public void TearDown()
        {
            Session.Dispose();
        }

        [Test]
        public void AddTest()
        {
            var parameters = new Neo4jParameters();

            var person = new Person
            {
                born = 1962,
                name = "Tom Cruise"
            };

            parameters.AddEntity("actor", person);

            try
            {
                Session.Run(@"CREATE (:Actor $actor)", parameters);

                var result = Session.Run(@"
                    MATCH (actor:Actor)
                    RETURN actor");

                var actors = result.Map<Person>().ToList();

                Assert.AreEqual(1, actors.Count);

                var tomCruise = actors.Single();

                Assert.AreEqual(1962, tomCruise.born);
                Assert.AreEqual("Tom Cruise", tomCruise.name);
            }
            finally
            {
                Session.Run(@"
                    MATCH (a:Actor)
                    DELETE a");
            }
        }
    }
}
