using System.Linq;
using IntegrationTests.Models;
using Neo4jMapper;
using NUnit.Framework;

namespace IntegrationTests.Tests
{
    [TestFixture]
    public class SessionExtensionsTests : MoviesFixtureBase
    {
        [Test]
        public void RunnerTest()
        {
            const string query = @"
                MATCH (person:Person {name: 'Cuba Gooding Jr.'})
                RETURN person";

            var result = Session.Run(query);

            var person = result.Return<Person>().SingleOrDefault();

            Assert.IsNotNull(person);

            person.born = 1901;

            const string updateQuery = @"
                MATCH (person:Person {name: 'Cuba Gooding Jr.'})
                SET person = $p1";

            Session.Run<Person>(updateQuery, p1 => person);

            result = Session.Run(query);

            person = result.Return<Person>().SingleOrDefault();

            Assert.IsNotNull(person);
            Assert.AreEqual(1901, person.born);
        }
    }
}
