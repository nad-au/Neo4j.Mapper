using System.Collections;
using System.Collections.Generic;
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
        public void BasicTest()
        {
            var result = session.Run(@"
                MATCH (person:Person)
                RETURN person
                LIMIT 10");

            var persons = result.Return<Person>().ToList();

            Assert.AreEqual(10, persons.Count);
        }

        [Test]
        public void AnotherTest()
        {
            var result = session.Run(@"
                MATCH (movie:Movie)
                RETURN movie
                LIMIT 10");

            var movies = result.Return<Movie>().ToList();

            Assert.AreEqual(10, movies.Count);
        }

        [Test]
        public void AndAnotherTest()
        {
            var result = session.Run(@"
                MATCH (person:Person)-[:ACTED_IN]->(movie:Movie)
                WITH person, COLLECT(movie) AS movies
                WHERE SIZE(movies) > 1
                RETURN person, movies
                LIMIT 10");

            var movies = result.Return<Person, IEnumerable<Movie>, Person>(((p, m) => p)).ToList();

            Assert.AreEqual(10, movies.Count);
        }

        public void AndYetAnotherTest()
        {
            if (typeof(IEnumerable).IsAssignableFrom(typeof(T)))
            {

            var specificListType = genericListType.MakeGenericType(typeof(double));
        }
    }
}
