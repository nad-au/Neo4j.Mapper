using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntegrationTests.Models;
using Neo4j.Mapper;
using NUnit.Framework;

namespace IntegrationTests.Tests
{
    [TestFixture]
    [NonParallelizable]
    public class AsyncBasicTests : MoviesFixtureBase
    {
        [Test]
        public async Task Should_Map_Person_Nodes()
        {
            var cursor = await Session.RunAsync(@"
                MATCH (person:Person)
                RETURN person
                LIMIT 10");

            var persons = await cursor.MapAsync<Person>();

            Assert.AreEqual(10, persons.Count);
        }

        [Test]
        public async Task Should_Map_Movie_Nodes()
        {
            var cursor = await Session.RunAsync(@"
                MATCH (movie:Movie)
                RETURN movie
                LIMIT 10");

            var movies = await cursor.MapAsync<Movie>();

            Assert.AreEqual(10, movies.Count);
            Assert.IsTrue(movies.All(p => p.Id != default));
        }

        [Test]
        public async Task Should_Map_Cypher_Maps()
        {
            var cursor = await Session.RunAsync(@"
                MATCH (movie:Movie)
                RETURN movie { .* }
                LIMIT 10");

            var movies = await cursor.MapAsync<Movie>();

            Assert.AreEqual(10, movies.Count);
        }

        [Test]
        public async Task Should_Map_List_Of_Movies()
        {
            var cursor = await Session.RunAsync(@"
                MATCH (movie:Movie)
                RETURN COLLECT(movie)");

            var movies = await cursor.MapSingleAsync<List<Movie>>();

            Assert.AreEqual(38, movies.Count);
        }

        [Test]
        public async Task Should_Map_List_Of_Cypher_Maps()
        {
            var cursor = await Session.RunAsync(@"
                MATCH (movie:Movie)
                RETURN COLLECT(movie { .* })");

            var movies = await cursor.MapSingleAsync<List<Movie>>();

            Assert.AreEqual(38, movies.Count);
        }

        [Test]
        public async Task Should_Map_Person_With_List_Of_Movie_Nodes()
        {
            var cursor = await Session.RunAsync(@"
                MATCH (person:Person {name: 'Cuba Gooding Jr.'})-[:ACTED_IN]->(movie:Movie)
                RETURN person, COLLECT(movie) AS movies");

            var actor = await cursor
                .MapSingleAsync<Person, IEnumerable<Movie>, Person>((person, movies) =>
            {
                person.MoviesActedIn = movies;
                return person;
            });

            Assert.AreEqual(4, actor.MoviesActedIn.Count());
            Assert.AreEqual(1968, actor.born);
            Assert.IsTrue(actor.MoviesActedIn.All(p => p.Id != default));
        }

        [Test]
        public async Task Should_Return_Anonymous_Type()
        {
            var cursor = await Session.RunAsync(@"
                MATCH (person:Person {name: 'Cuba Gooding Jr.'})-[:ACTED_IN]->(movie:Movie)
                RETURN person, COLLECT(movie) AS movies");

            var actor = await cursor
                .MapSingleAsync((Person person, IEnumerable<Movie> movies) => new
            {
                Person = person,
                Movies = movies
            });

            Assert.AreEqual("Cuba Gooding Jr.", actor.Person.name);
            Assert.AreEqual(1968, actor.Person.born);
            Assert.AreEqual(4, actor.Movies.Count());
        }

        [Test]
        public async Task Should_Return_Projection()
        {
            var cursor = await Session.RunAsync(@"
                MATCH (person:Person {name: 'Cuba Gooding Jr.'})-[:ACTED_IN]->(movie:Movie)
                RETURN person.name, COLLECT(movie) AS movies");

            var actor = await cursor
                .MapSingleAsync((string actorName, IEnumerable<Movie> movies) => new ActorName
            {
                name = actorName,
                MoviesActedIn = movies
            });

            Assert.AreEqual("Cuba Gooding Jr.", actor.name);
            Assert.AreEqual(4, actor.MoviesActedIn.Count());
        }

        [Test]
        public async Task Should_Map_Cypher_Map_Containing_List_Of_Maps()
        {
            var cursor = await Session.RunAsync(@"
                MATCH (person:Person {name: 'Cuba Gooding Jr.'})-[:ACTED_IN]->(movie:Movie)
                RETURN person { .name, moviesActedIn: COLLECT(movie { .title, .released })}");

            var actorWithMovies = await cursor.MapSingleAsync<Person>();

            Assert.AreEqual(4, actorWithMovies.MoviesActedIn.Count());
        }

        [Test]
        public async Task SingleAsync_Throws_InvalidOperationException_With_No_Result()
        {
            var cursor = await Session.RunAsync(@"
                MATCH (person:Person {name: 'Mickey Mouse'})
                RETURN person");

            var exception = Assert.ThrowsAsync<InvalidOperationException>(
                async () => await cursor.MapSingleAsync<Person>());

            Assert.AreEqual("The result is empty.", exception.Message);
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

        [Test]
        public async Task Should_Map_Relationship_Properties()
        {
            try
            {
                var cursor = await Session.RunAsync(@"
                    CREATE (:Person)-[knows:KNOWS]->()
                    SET knows = {relationship: 'Brother',current:true}
                    RETURN knows");

                var knows = await cursor.MapSingleAsync<Knows>();

                Assert.AreEqual("Brother", knows.relationship);
                Assert.IsTrue(knows.current);
            }
            finally
            {
                await Session.RunAsync(@"
                    MATCH (person:Person)-[knows:KNOWS]->()
                    DELETE person, knows");
            }
        }
    }
}
