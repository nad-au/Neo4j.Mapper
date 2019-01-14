using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using Benchmarks.Models;
using Neo4jMapper;

namespace Benchmarks
{
    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    public class MapperComparisonBenchmarks : BenchmarksBase
    {
        #region Movies

        [BenchmarkCategory("Movies"), Benchmark]
        public List<Movie> Movies_N4JM()
        {
            using (var session = Driver.Session())
            {
                var result = session.Run(@"
                    MATCH (movie:Movie)
                    RETURN movie");

                return result.Map<Movie>().ToList();
            }
        }

        [BenchmarkCategory("Movies"), Benchmark(Baseline = true)]
        public List<Movie> Movies_N4JC()
        {
            var cypher = GraphClient
                .Cypher
                .Match("(movie:Movie)")
                .Return<Movie>("movie");

            return cypher.Results.ToList();
        }

        [BenchmarkCategory("Movies"), Benchmark]
        public List<Movie> Movies_N4JB()
        {
            var cypher = BoltGraphClient
                .Cypher
                .Match("(movie:Movie)")
                .Return<Movie>("movie");

            return cypher.Results.ToList();
        }

        #endregion

        #region Actors With Movies Starred In

        [BenchmarkCategory("ActorsWithMoviesStarredIn"), Benchmark]
        public List<Person> ActorsWithMoviesStarredIn_N4JM()
        {
            using (var session = Driver.Session())
            {
                var result = session.Run(@"
                    MATCH (person)-[:ACTED_IN]->(movie:Movie)
                    RETURN person, COLLECT(movie) AS movies");

                return result.Map<Person, IEnumerable<Movie>, Person>((person, movies) =>
                {
                    person.MovesActedIn = movies;
                    return person;
                }).ToList();
            }
        }

        [BenchmarkCategory("ActorsWithMoviesStarredIn"), Benchmark(Baseline = true)]
        public List<Person> ActorsWithMoviesStarredIn_N4JC()
        {
            var cypher = GraphClient
                .Cypher
                .Match("(person)-[:ACTED_IN]->(movie:Movie)")
                .Return((person, movie) => new
                {
                    Person = person.As<Person>(),
                    Movies = movie.CollectAs<Movie>()
                });

            return cypher.Results.Select(r =>
            {
                r.Person.MovesActedIn = r.Movies.ToList();
                return r.Person;
            }).ToList();
        }

        [BenchmarkCategory("ActorsWithMoviesStarredIn"), Benchmark]
        public List<Person> ActorsWithMoviesStarredIn_N4JB()
        {
            var cypher = BoltGraphClient
                .Cypher
                .Match("(person)-[:ACTED_IN]->(movie:Movie)")
                .Return((person, movie) => new
                {
                    Person = person.As<Person>(),
                    Movies = movie.CollectAs<Movie>()
                });

            return cypher.Results.Select(r =>
            {
                r.Person.MovesActedIn = r.Movies.ToList();
                return r.Person;
            }).ToList();
        }

        #endregion
    }
}