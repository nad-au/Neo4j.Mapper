using System.Collections.Generic;
using System.Linq;
using IntegrationTests.Models;
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

            var map = result.Map<MapModel>().SingleOrDefault();

            Assert.IsNotNull(map);
            Assert.AreEqual("Value", map.Key);
            Assert.IsNotNull(map.Inner);
            Assert.AreEqual("Map1", map.Inner.Item);
        }

        [Test]
        public void MapWithListTest()
        {
            var result = Session.Run(@"
                RETURN { key: 'Value', listKey: [{ item: 'Map1' }, { item: 'Map2' }]}");

            var map = result.Map<MapWithListModel>().SingleOrDefault();

            Assert.IsNotNull(map);
            Assert.AreEqual("Value", map.Key);
            Assert.AreEqual(2, map.ListKey.Count);
            Assert.AreEqual("Map1", map.ListKey.First().Item);
            Assert.AreEqual("Map2", map.ListKey.Last().Item);
        }

        [Test]
        public void PersonsTest()
        {
            var result = Session.Run(@"
                MATCH (person:Person)
                RETURN person
                LIMIT 10");

            var persons = result.Map<Person>().ToList();

            Assert.AreEqual(10, persons.Count);
        }

        [Test]
        public void MoviesTest()
        {
            var result = Session.Run(@"
                MATCH (movie:Movie)
                RETURN movie
                LIMIT 10");

            var movies = result.Map<Movie>().ToList();

            Assert.AreEqual(10, movies.Count);
            Assert.IsTrue(movies.All(p => p.Id != default(long)));
        }

        [Test]
        public void MoviesMapTest()
        {
            var result = Session.Run(@"
                MATCH (movie:Movie)
                RETURN movie { .* }
                LIMIT 10");

            var movies = result.Map<Movie>().ToList();

            Assert.AreEqual(10, movies.Count);
        }

        [Test]
        public void LiteralListTest()
        {
            var result = Session.Run(@"
                RETURN range(0, 10)[..4]");

            var sequence = result.Map<List<byte>>().SingleOrDefault();

            Assert.IsNotNull(sequence);
            Assert.AreEqual(4, sequence.Count);
            Assert.AreEqual(0, sequence.First());
            Assert.AreEqual(3, sequence.Last());
        }

        [Test]
        public void ListOfMoviesTest()
        {
            var result = Session.Run(@"
                MATCH (movie:Movie)
                RETURN COLLECT(movie)");

            var movies = result.Map<List<Movie>>().SingleOrDefault();

            Assert.IsNotNull(movies);
            Assert.AreEqual(38, movies.Count);
        }

        [Test]
        public void ListOfMoviesMapTest()
        {
            var result = Session.Run(@"
                MATCH (movie:Movie)
                RETURN COLLECT(movie { .* })");

            var movies = result.Map<List<Movie>>().SingleOrDefault();

            Assert.IsNotNull(movies);
            Assert.AreEqual(38, movies.Count);
        }

        [Test]
        public void ActorWithListOfMovies()
        {
            var result = Session.Run(@"
                MATCH (person:Person {name: 'Cuba Gooding Jr.'})-[:ACTED_IN]->(movie:Movie)
                WITH person, COLLECT(movie) AS movies
                RETURN person, movies");

            var actor = result.Map<Person, IEnumerable<Movie>, Person>((person, movies) =>
            {
                person.MovesActedIn = movies;
                return person;
            }).SingleOrDefault();

            Assert.IsNotNull(actor);
            Assert.AreEqual(4, actor.MovesActedIn.Count());
            Assert.IsTrue(actor.MovesActedIn.All(p => p.Id != default(long)));
        }

        [Test]
        public void AnonymousTypeWithActorAndListOfMovies()
        {
            var result = Session.Run(@"
                MATCH (person:Person {name: 'Cuba Gooding Jr.'})-[:ACTED_IN]->(movie:Movie)
                WITH person, COLLECT(movie) AS movies
                RETURN person, movies");

            var anonType = result.Map((Person person, IEnumerable<Movie> movies) => new
            {
                Person = person,
                Movies = movies
            }).SingleOrDefault();

            Assert.IsNotNull(anonType);
            Assert.IsNotNull(anonType.Person);
            Assert.AreEqual(4, anonType.Movies.Count());
        }

        [Test]
        public void ActorNameWithListOfMovies()
        {
            var result = Session.Run(@"
                MATCH (person:Person {name: 'Cuba Gooding Jr.'})-[:ACTED_IN]->(movie:Movie)
                WITH person, COLLECT(movie) AS movies
                RETURN person.name, movies");

            var actor = result.Map<string, IEnumerable<Movie>, ActorName>((actorName, movies) => new ActorName
            {
                name = actorName,
                MovesActedIn = movies
            }).SingleOrDefault();

            Assert.IsNotNull(actor);
            Assert.AreEqual(4, actor.MovesActedIn.Count());
        }

        [Test]
        public void MapOfActorNameWithListOfMovies()
        {
            var result = Session.Run(@"
                MATCH (person:Person {name: 'Cuba Gooding Jr.'})-[:ACTED_IN]->(movie:Movie)
                RETURN person { .name, movesActedIn: COLLECT(movie { .title, .released })}");

            var actorWithMovies = result.Map<Person>().SingleOrDefault();

            Assert.IsNotNull(actorWithMovies);
            Assert.AreEqual(4, actorWithMovies.MovesActedIn.Count());
        }
    }
}
