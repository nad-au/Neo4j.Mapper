using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntegrationTests.Models;
using Neo4jMapper;
using NUnit.Framework;

namespace IntegrationTests.Tests
{
    [TestFixture]
    [NonParallelizable]
    public class AsyncEnumerableTests : MoviesFixtureBase
    {
        [Test]
        public async Task Should_Map_Cypher_Map_With_Inner_Item()
        {
            var asyncResults = (await Session.RunAsync(@"
                RETURN { name: 'Foo', otherName: { name: 'Fu'}}"))
                .AsyncResults();

            var map = await asyncResults.Map<PersonIdentity>().SingleAsync();

            Assert.AreEqual("Foo", map.Name);
            Assert.IsNotNull(map.OtherName);
            Assert.AreEqual("Fu", map.OtherName.Name);
        }

        [Test]
        public async Task Should_Map_Literal_List()
        {
            var asyncResults = (await Session.RunAsync(@"
                RETURN range(0, 10)[..4]"))
                .AsyncResults();

            var sequence = await asyncResults.Map<List<byte>>().SingleAsync();

            Assert.AreEqual(4, sequence.Count);
            Assert.AreEqual(0, sequence.First());
            Assert.AreEqual(3, sequence.Last());
        }

        [Test]
        public async Task Should_Map_Cypher_Map_With_Inner_List()
        {
            var asyncResults = (await Session.RunAsync(@"
                RETURN { name: 'Foo', otherNames: [{ name: 'Fu' }, { name: 'Fuey' }]}"))
                .AsyncResults();

            var map = await asyncResults.Map<PersonWithIdentities>().SingleAsync();

            Assert.AreEqual("Foo", map.Name);
            Assert.AreEqual(2, map.OtherNames.Count);
            Assert.AreEqual("Fu", map.OtherNames.First().Name);
            Assert.AreEqual("Fuey", map.OtherNames.Last().Name);
        }

        [Test]
        public async Task Should_Map_Person_Nodes()
        {
            var asyncResults = (await Session.RunAsync(@"
                MATCH (person:Person)
                RETURN person
                LIMIT 10")).AsyncResults();

            var persons = await asyncResults.Map<Person>().ToListAsync();

            Assert.AreEqual(10, persons.Count);
        }

        [Test]
        public async Task Should_Map_Movie_Nodes()
        {
            var asyncResults = (await Session.RunAsync(@"
                MATCH (movie:Movie)
                RETURN movie
                LIMIT 10")).AsyncResults();

            var movies = await asyncResults.Map<Movie>().Take(4).ToListAsync();

            Assert.AreEqual(4, movies.Count);
            Assert.IsTrue(movies.All(p => p.Id != default));
        }

        [Test]
        public async Task Should_Map_Movie_Nodes_With_Linq_TakeWhile()
        {
            var asyncResults = (await Session.RunAsync(@"
                MATCH (movie:Movie)
                RETURN movie
                ORDER BY movie.released")).AsyncResults();

            var movies = await asyncResults.Map<Movie>().TakeWhile(m => m.released < 2000).ToListAsync();

            Assert.AreEqual(23, movies.Count);
            Assert.IsTrue(movies.Last().released == 1999);
        }

        [Test]
        public async Task Should_Map_Cypher_Maps()
        {
            var asyncResults = (await Session.RunAsync(@"
                MATCH (movie:Movie)
                RETURN movie { .* }
                LIMIT 10")).AsyncResults();

            var movies = await asyncResults.Map<Movie>().Take(4).ToListAsync(); 

            Assert.AreEqual(4, movies.Count);
        }

        [Test]
        public async Task Should_Map_List_Of_Movies()
        {
            var asyncResults = (await Session.RunAsync(@"
                MATCH (movie:Movie)
                RETURN COLLECT(movie)")).AsyncResults();

            var movies = await asyncResults.Map<List<Movie>>().SingleAsync();

            Assert.AreEqual(38, movies.Count);
        }

        [Test]
        public async Task Should_Map_List_Of_Cypher_Maps()
        {
            var asyncResults = (await Session.RunAsync(@"
                MATCH (movie:Movie)
                RETURN COLLECT(movie { .* })")).AsyncResults();

            var movies = await asyncResults.Map<List<Movie>>().SingleAsync();

            Assert.AreEqual(38, movies.Count);
        }

        [Test]
        public async Task Should_Map_Person_With_List_Of_Movie_Nodes()
        {
            var asyncResults = (await Session.RunAsync(@"
                MATCH (person:Person {name: 'Cuba Gooding Jr.'})-[:ACTED_IN]->(movie:Movie)
                RETURN person, COLLECT(movie) AS movies")).AsyncResults();

            var actor = await asyncResults
                .Map<Person, IEnumerable<Movie>, Person>((person, movies) =>
            {
                person.MoviesActedIn = movies;
                return person;
            }).SingleAsync();

            Assert.AreEqual(4, actor.MoviesActedIn.Count());
            Assert.AreEqual(1968, actor.born);
            Assert.IsTrue(actor.MoviesActedIn.All(p => p.Id != default));
        }

        [Test]
        public async Task Should_Return_Anonymous_Type()
        {
            var asyncResults = (await Session.RunAsync(@"
                MATCH (person:Person {name: 'Cuba Gooding Jr.'})-[:ACTED_IN]->(movie:Movie)
                RETURN person, COLLECT(movie) AS movies")).AsyncResults();

            var actor = await asyncResults
                .Map((Person person, IEnumerable<Movie> movies) => new
            {
                Person = person,
                Movies = movies
            }).SingleAsync();

            Assert.AreEqual("Cuba Gooding Jr.", actor.Person.name);
            Assert.AreEqual(1968, actor.Person.born);
            Assert.AreEqual(4, actor.Movies.Count());
        }

        [Test]
        public async Task Should_Return_Projection()
        {
            var asyncResults = (await Session.RunAsync(@"
                MATCH (person:Person {name: 'Cuba Gooding Jr.'})-[:ACTED_IN]->(movie:Movie)
                RETURN person.name, COLLECT(movie) AS movies")).AsyncResults();

            var actor = await asyncResults
                .Map((string actorName, IEnumerable<Movie> movies) => new ActorName
            {
                name = actorName,
                MoviesActedIn = movies
            }).SingleAsync();

            Assert.AreEqual("Cuba Gooding Jr.", actor.name);
            Assert.AreEqual(4, actor.MoviesActedIn.Count());
        }

        [Test]
        public async Task Should_Map_Cypher_Map_Containing_List_Of_Maps()
        {
            var asyncResults = (await Session.RunAsync(@"
                MATCH (person:Person {name: 'Cuba Gooding Jr.'})-[:ACTED_IN]->(movie:Movie)
                RETURN person { .name, moviesActedIn: COLLECT(movie { .title, .released })}"))
                .AsyncResults();

            var actorWithMovies = await asyncResults.Map<Person>().SingleAsync();

            Assert.AreEqual(4, actorWithMovies.MoviesActedIn.Count());
        }

        [Test]
        public async Task SingleAsync_Throws_InvalidOperationException_With_No_Result()
        {
            var asyncResults = (await Session.RunAsync(@"
                MATCH (person:Person {name: 'Mickey Mouse'})
                RETURN person")).AsyncResults();

            var exception = Assert.ThrowsAsync<InvalidOperationException>(
                async () => await asyncResults.Map<Person>().SingleAsync());

            Assert.AreEqual("Source sequence doesn't contain any elements.", exception.Message);
        }

        [Test]
        public async Task ToListAsync_Should_Not_Throw_With_Empty_Results()
        {
            var cursor = await Session.RunAsync(@"
                MATCH (person:Person {name: 'Mickey Mouse'})
                RETURN person");

            var persons = await cursor.MapAsync<Person>();

            Assert.IsEmpty(persons);
        }
    }
}
