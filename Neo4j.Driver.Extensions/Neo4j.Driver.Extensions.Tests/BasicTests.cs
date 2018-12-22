﻿using System.Collections.Generic;
using System.Linq;
using Neo4j.Driver.Extensions.Tests.Models;
using Neo4j.Driver.Extensions.Tests.Queries;
using Neo4j.Driver.V1;
using NUnit.Framework;

namespace Neo4j.Driver.Extensions.Tests
{
    [TestFixture]
    public class BasicTests : TestFixtureBase
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
        public void PersonsTest()
        {
            var result = session.Run(@"
                MATCH (person:Person)
                RETURN person
                LIMIT 10");

            var persons = result.Return<Person>().ToList();

            Assert.AreEqual(10, persons.Count);
        }

        [Test]
        public void MoviesTest()
        {
            var result = session.Run(@"
                MATCH (movie:Movie)
                RETURN movie
                LIMIT 10");

            var movies = result.Return<Movie>().ToList();

            Assert.AreEqual(10, movies.Count);
        }

        [Test]
        public void ListOfMoviesTest()
        {
            var result = session.Run(@"
                MATCH (movie:Movie)
                RETURN COLLECT(movie)");

            var movies = result.Return<List<Movie>>().SingleOrDefault();

            Assert.IsNotNull(movies);
            Assert.AreEqual(38, movies.Count);
        }

        [Test]
        public void ActorWithListOfMoves()
        {
            var result = session.Run(@"
                MATCH (person:Person {name: 'Cuba Gooding Jr.'})-[:ACTED_IN]->(movie:Movie)
                WITH person, COLLECT(movie) AS movies
                RETURN person, movies");

            var actor = result.Return<Person, IEnumerable<Movie>, Person>((person, movies) =>
            {
                person.MovesActedIn = movies;
                return person;
            }).SingleOrDefault();

            Assert.IsNotNull(actor);
            Assert.AreEqual(4, actor.MovesActedIn.Count());
        }
    }
}
