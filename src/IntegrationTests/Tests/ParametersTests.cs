using System;
using System.Threading.Tasks;
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
        public async Task Should_Create_New_Actor_From_Entity_Parameter()
        {
            var person = new Person
            {
                born = 1962,
                name = "Tom Cruise"
            };

            var parameters = new Neo4jParameters().WithEntity("actor", person);

            try
            {
                await Session.RunAsync(@"CREATE (:Actor $actor)", parameters);

                var cursor = await Session.RunAsync(@"
                    MATCH (actor:Actor)
                    RETURN actor");

                var tomCruise = await cursor.MapSingleAsync<Person>();

                Assert.AreEqual(1962, tomCruise.born);
                Assert.AreEqual("Tom Cruise", tomCruise.name);
            }
            catch (Exception exception)
            {
                Assert.Fail(exception.Message);
            }
            finally
            {
                await Session.RunAsync(@"
                    MATCH (a:Actor)
                    DELETE a");
            }
        }
    }
}
