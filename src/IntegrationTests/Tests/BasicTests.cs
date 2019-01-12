using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntegrationTests.Models;
using Neo4j.Driver.V1;
using Neo4jMapper;
using NUnit.Framework;

namespace IntegrationTests.Tests
{
    [TestFixture]
    public class BasicTests : MoviesFixtureBase
    {
        [Test]
        public void MapTest()
        {
            var result = Session.Run(@"
                RETURN { key: 'Value', inner: { item: 'Map1'}}");

            var map = result.Single().Map<MapModel>();

            Assert.AreEqual("Value", map.Key);
            Assert.IsNotNull(map.Inner);
            Assert.AreEqual("Map1", map.Inner.Item);
        }

        [Test]
        public void LiteralListTest()
        {
            var result = Session.Run(@"
                RETURN range(0, 10)[..4]");

            var sequence = result.Single().Map<List<byte>>();

            Assert.AreEqual(4, sequence.Count);
            Assert.AreEqual(0, sequence.First());
            Assert.AreEqual(3, sequence.Last());
        }

        [Test]
        public void MapWithListTest()
        {
            var result = Session.Run(@"
                RETURN { key: 'Value', listKey: [{ item: 'Map1' }, { item: 'Map2' }]}");

            var map = result.Single().Map<MapWithListModel>();

            Assert.AreEqual("Value", map.Key);
            Assert.AreEqual(2, map.ListKey.Count);
            Assert.AreEqual("Map1", map.ListKey.First().Item);
            Assert.AreEqual("Map2", map.ListKey.Last().Item);
        }

        [Test]
        public async Task PersonsTest()
        {
            var cursor = await Session.RunAsync(@"
                MATCH (person:Person)
                RETURN person
                LIMIT 10");

            var persons = await cursor.ToListAsync(record => record.Map<Person>());

            Assert.AreEqual(10, persons.Count);
        }

        [Test]
        public async Task MoviesTest()
        {
            var cursor = await Session.RunAsync(@"
                MATCH (movie:Movie)
                RETURN movie
                LIMIT 10");

            var movies = await cursor.ToListAsync(record => record.Map<Movie>());

            Assert.AreEqual(10, movies.Count);
            Assert.IsTrue(movies.All(p => p.Id != default(long)));
        }

        [Test]
        public async Task MoviesMapTest()
        {
            var cursor = await Session.RunAsync(@"
                MATCH (movie:Movie)
                RETURN movie { .* }
                LIMIT 10");

            var movies = await cursor.ToListAsync(record => record.Map<Movie>());

            Assert.AreEqual(10, movies.Count);
        }

        [Test]
        public async Task ListOfMoviesTest()
        {
            var cursor = await Session.RunAsync(@"
                MATCH (movie:Movie)
                RETURN COLLECT(movie)");

            var movies = (await cursor.SingleAsync()).Map<List<Movie>>();

            Assert.AreEqual(38, movies.Count);
        }

        [Test]
        public async Task ListOfMoviesMapTest()
        {
            var cursor = await Session.RunAsync(@"
                MATCH (movie:Movie)
                RETURN COLLECT(movie { .* })");

            var movies = (await cursor.SingleAsync()).Map<List<Movie>>();

            Assert.IsNotNull(movies);
            Assert.AreEqual(38, movies.Count);
        }

        [Test]
        public async Task ActorWithListOfMovies()
        {
            var cursor = await Session.RunAsync(@"
                MATCH (person:Person {name: 'Cuba Gooding Jr.'})-[:ACTED_IN]->(movie:Movie)
                WITH person, COLLECT(movie) AS movies
                RETURN person, movies");

            var actor = (await cursor.SingleAsync())
                .Map<Person, IEnumerable<Movie>, Person>((person, movies) =>
            {
                person.MoviesActedIn = movies;
                return person;
            });

            Assert.IsNotNull(actor);
            Assert.AreEqual(4, actor.MoviesActedIn.Count());
            Assert.IsTrue(actor.MoviesActedIn.All(p => p.Id != default(long)));
        }

        [Test]
        public async Task AnonymousTypeWithActorAndListOfMovies()
        {
            var cursor = await Session.RunAsync(@"
                MATCH (person:Person {name: 'Cuba Gooding Jr.'})-[:ACTED_IN]->(movie:Movie)
                WITH person, COLLECT(movie) AS movies
                RETURN person, movies");

            var actor = (await cursor.SingleAsync())
                .Map((Person person, IEnumerable<Movie> movies) => new
            {
                Person = person,
                Movies = movies
            });

            Assert.AreEqual("Cuba Gooding Jr.", actor.Person.name);
            Assert.AreEqual(1968, actor.Person.born);
            Assert.AreEqual(4, actor.Movies.Count());
        }

        [Test]
        public async Task ActorNameWithListOfMovies()
        {
            var cursor = await Session.RunAsync(@"
                MATCH (person:Person {name: 'Cuba Gooding Jr.'})-[:ACTED_IN]->(movie:Movie)
                WITH person, COLLECT(movie) AS movies
                RETURN person.name, movies");

            var actor = (await cursor.SingleAsync())
                .Map((string actorName, IEnumerable<Movie> movies) => new ActorName
            {
                name = actorName,
                MoviesActedIn = movies
            });

            Assert.AreEqual("Cuba Gooding Jr.", actor.name);
            Assert.AreEqual(4, actor.MoviesActedIn.Count());
        }

        [Test]
        public async Task MapOfActorNameWithListOfMovies()
        {
            var cursor = await Session.RunAsync(@"
                MATCH (person:Person {name: 'Cuba Gooding Jr.'})-[:ACTED_IN]->(movie:Movie)
                RETURN person { .name, moviesActedIn: COLLECT(movie { .title, .released })}");

            var actorWithMovies = (await cursor.SingleAsync()).Map<Person>();

            Assert.IsNotNull(actorWithMovies);
            Assert.AreEqual(4, actorWithMovies.MoviesActedIn.Count());
        }
    }
}
