using System.Linq;
using FluentAssertions;
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

        [Test]
        public void GetNodeShouldPopulateNodeId()
        {
            var result = Session.Run(@"
                MATCH (movie:Movie {title: 'Top Gun'})
                RETURN movie");

            var movie = result.Return<Movie>().SingleOrDefault();

            Assert.IsNotNull(movie);
            Assert.AreNotEqual(movie.Id, default(long));

            // Act
            var node = Session.GetNode<Movie>(movie.Id);

            node.Should().BeEquivalentTo(movie);
        }

        [Test]
        public void SetNodeShouldUpdateValues()
        {
            var result = Session.Run(@"
                MATCH (movie:Movie {title: 'Top Gun'})
                RETURN movie");

            var movie = result.Return<Movie>().SingleOrDefault();

            Assert.IsNotNull(movie);
            Assert.AreNotEqual(movie.Id, default(long));

            movie.title = "Top Gun 2";

            // Act
            Session.UpdateNode(movie);

            var node = Session.GetNode<Movie>(movie.Id);

            node.Should().BeEquivalentTo(movie);
        }
    }
}
