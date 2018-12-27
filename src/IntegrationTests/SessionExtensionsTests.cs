using System.Linq;
using IntegrationTests.Models;
using IntegrationTests.Queries;
using Neo4j.Driver.V1;
using Neo4jMapper;
using NUnit.Framework;

namespace IntegrationTests
{
    [TestFixture]
    public class SessionExtensionsTests : TestFixtureBase
    {
        private ISession session;

        [SetUp]
        public void SetUp()
        {
            session = Driver.Session();
            session.Run(Query.CreateMovies);
        }

        [TearDown]
        public void TearDown()
        {
            session.Run(Query.DeleteMovies);
            session.Dispose();
        }

        [Test]
        public void RunnerTest()
        {
            const string query = @"
                MATCH (person:Person {name: 'Cuba Gooding Jr.'})
                RETURN person";

            var result = session.Run(query);

            var person = result.Return<Person>().SingleOrDefault();

            Assert.IsNotNull(person);

            person.born = 1901;

            const string updateQuery = @"
                MATCH (person:Person {name: 'Cuba Gooding Jr.'})
                SET person = $p1";

            session.Run<Person>(updateQuery, p1 => person);

            result = session.Run(query);

            person = result.Return<Person>().SingleOrDefault();

            Assert.IsNotNull(person);
            Assert.AreEqual(1901, person.born);
        }
    }
}
