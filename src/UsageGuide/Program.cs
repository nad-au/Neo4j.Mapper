using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Neo4j.Driver.V1;
using Neo4jMapper;
using ServiceStack.Text;
using UsageGuide.Models;

namespace UsageGuide
{
    class Program
    {
        static void Main(string[] args)
        {
            //Example11();
            Task.Run(Example13).Wait();

            Console.WriteLine("Done. Press <Enter> to end.");
            Console.ReadLine();
        }

        static void Example1()
        {
            using (var driver = GraphDatabase.Driver("bolt://localhost:7687"))
            {
                using (var session = driver.Session())
                {
                    var result = session.Run(@"
                        MATCH (person:Person)
                        RETURN person
                        LIMIT 1");

                    foreach (var record in result)
                    {
                        var output = record.Values.Dump();
                        Console.WriteLine(output);
                    }
                }
            }
        }

        static async Task Example2()
        {
            using (var driver = GraphDatabase.Driver("bolt://localhost:7687"))
            {
                using (var session = driver.Session())
                {
                    var cursor = await session.RunAsync(@"
                        MATCH (movie:MovieEntity)
                        RETURN movie
                        LIMIT 1");

                    foreach (var record in (await cursor.ToListAsync()))
                    {
                        var output = record.Values.Dump();
                        Console.WriteLine(output);
                    }
                }
            }
        }

        static async Task Example3()
        {
            using (var driver = GraphDatabase.Driver("bolt://localhost:7687"))
            {
                using (var session = driver.Session())
                {
                    var cursor = await session.RunAsync(@"
                        MATCH (person:Person)
                        RETURN person
                        LIMIT 1");

                    var person = (await cursor.SingleAsync()).Map<Person>();

                    var output = person.Dump();
                    Console.WriteLine(output);
                }
            }
        }

        static async Task Example4()
        {
            using (var driver = GraphDatabase.Driver("bolt://localhost:7687"))
            {
                using (var session = driver.Session())
                {
                    var cursor = await session.RunAsync(@"
                        MATCH (actor:Person {name: 'Cuba Gooding Jr.'})-[:ACTED_IN]->(movie:Movie)
                        WITH actor, movie
                        ORDER BY movie.released
                        WITH actor, collect(movie) AS movies
                        RETURN actor, head(movies) AS firstMovie");

                    var actorWithMovie = (await cursor.SingleAsync()).Map((Person actor, Movie firstMovie) => new
                    {
                        Actor = actor,
                        FirstMovie = firstMovie
                    });

                    var output = actorWithMovie.Dump();
                    Console.WriteLine(output);
                }
            }
        }

        static async Task Example5()
        {
            using (var driver = GraphDatabase.Driver("bolt://localhost:7687"))
            {
                using (var session = driver.Session())
                {
                    var cursor = await session.RunAsync(@"
                        MATCH (actor:Person)
                        WITH actor
                        ORDER BY actor.born
                        LIMIT 2
                        MATCH (actor)-[:ACTED_IN]->(movie:Movie)
                        WITH actor, movie
                        ORDER BY movie.released DESC
                        WITH actor, collect(movie) AS movies
                        RETURN actor, head(movies) AS newestMovie");

                    var actorWithMovies = (await cursor.ToListAsync()).Map((Person actor, Movie newestMovie) => new
                    {
                        Actor = actor,
                        NewestMovie = newestMovie
                    }).ToList();

                    var output = actorWithMovies.Dump();
                    Console.WriteLine(output);
                }
            }
        }

        class Example6Projection
        {
            public int NumberOfMovies { get; set; }
            public string ActorName { get; set; }
            public Person Actor { get; set; }
            public bool HasMovies { get; set; }
            public List<Movie> Movies { get; set; }
        }

        static async Task Example6()
        {
            using (var driver = GraphDatabase.Driver("bolt://localhost:7687"))
            {
                using (var session = driver.Session())
                {
                    var cursor = await session.RunAsync(@"
                        MATCH (actor:Person {name: 'Cuba Gooding Jr.'})-[:ACTED_IN]->(movie:Movie)
                        WITH count(movie) as countOfMovies, actor.name as actorName, actor, COLLECT(movie) as movies
                        RETURN countOfMovies, actorName, actor, size(movies) > 0 as hasMovies, movies");

                    var actorWithMovies =
                        (await cursor.SingleAsync()).Map<int, string, Person, bool, List<Movie>, Example6Projection>(
                            (numOfMovies, actorName, person, hasMovies, movies) => new Example6Projection
                            {
                                NumberOfMovies = numOfMovies,
                                ActorName = actorName,
                                Actor = person,
                                HasMovies = hasMovies,
                                Movies = movies
                            });

                    var output = actorWithMovies.Dump();
                    Console.WriteLine(output);
                }
            }
        }

        class Example7CustomType
        {
            public DateTimeOffset TemporalValue { get; set; }
        }
            
        // Should fail
        static async Task Example7()
        {
            using (var driver = GraphDatabase.Driver("bolt://localhost:7687"))
            {
                using (var session = driver.Session())
                {
                    var cursor = await session.RunAsync(@"RETURN { temporalValue: datetime() } as map");

                    var customType = (await cursor.SingleAsync()).Map<Example7CustomType>();

                    var output = customType.Dump();
                    Console.WriteLine(output);
                }
            }
        }

        class Example8CustomType
        {
            public ZonedDateTime TemporalValue { get; set; }
        }
            
        static async Task Example8()
        {
            using (var driver = GraphDatabase.Driver("bolt://localhost:7687"))
            {
                using (var session = driver.Session())
                {
                    var cursor = await session.RunAsync(@"RETURN { temporalValue: datetime() } as map");

                    var customType = (await cursor.SingleAsync()).Map<Example8CustomType>();

                    var output = customType.Dump();
                    Console.WriteLine(output);
                }
            }
        }

        class IMDBMovie : Movie
        {
            public string imdb { get; set; }
        }

        static async Task Example9()
        {
            using (var driver = GraphDatabase.Driver("bolt://localhost:7687"))
            {
                using (var session = driver.Session())
                {
                    var parameters = new Dictionary<string, object>
                    {
                        {"imdb", "tt0133093"}
                    };

                    var cursor = await session.RunAsync(@"
                        MATCH (movie:Movie {title: 'The Matrix'})
                        SET movie.imdb = $imdb
                        RETURN movie", parameters);

                    var updatedMovie = (await cursor.SingleAsync()).Map<IMDBMovie>();

                    var output = updatedMovie.Dump();
                    Console.WriteLine(output);
                }
            }
        }

        static async Task Example10()
        {
            using (var driver = GraphDatabase.Driver("bolt://localhost:7687"))
            {
                using (var session = driver.Session())
                {
                    var parameters = new Neo4jParameters
                    {
                        {"titleSearch", "Top Gun"},
                        {"imdb", "tt0092099"}
                    };

                    var cursor = await session.RunAsync(@"
                        MATCH (movie:Movie {title: $titleSearch})
                        SET movie.imdb = $imdb
                        RETURN movie", parameters);

                    var updatedMovie = (await cursor.SingleAsync()).Map<IMDBMovie>();

                    var output = updatedMovie.Dump();
                    Console.WriteLine(output);
                }
            }
        }

        static void Example11()
        {
            var movie = new IMDBMovie
            {
                imdb = "tt0110912",
                released = 1994,
                tagline =
                    "The lives of two mob hitmen, a boxer, a gangster's wife, and a pair of diner bandits intertwine in four tales of violence and redemption.",
                title = "Pulp Fiction"
            };

            var map = movie.ToParameterMap("newMovie");

            var output = map.Dump();
            Console.WriteLine(output);
        }

        static async Task Example12()
        {
            using (var driver = GraphDatabase.Driver("bolt://localhost:7687"))
            {
                using (var session = driver.Session())
                {
                    var movie = new IMDBMovie
                    {
                        imdb = "tt0110912",
                        released = 1994,
                        tagline =
                            "The lives of two mob hitmen, a boxer, a gangster's wife, and a pair of diner bandits intertwine in four tales of violence and redemption.",
                        title = "Pulp Fiction"
                    };

                    var parameters = new Neo4jParameters()
                        .WithEntity(movie, "newMovie");

                    var cursor = await session.RunAsync(@"
                        CREATE (movie:Movie $newMovie)
                        RETURN movie", parameters);

                    var createdMovie = (await cursor.SingleAsync()).Map<IMDBMovie>();

                    var output = createdMovie.Dump();
                    Console.WriteLine(output);
                }
            }
        }

        static async Task Example13()
        {
            using (var driver = GraphDatabase.Driver("bolt://localhost:7687"))
            {
                using (var session = driver.Session())
                {
                    var actorWithMovies = new Person
                    {
                        name = "Samuel L. Jackson",
                        born = 1948,
                        MoviesActedIn = new List<Movie>
                        {
                            new Movie
                            {
                                released = 1994,
                                tagline =
                                    "The lives of two mob hitmen, a boxer, a gangster's wife, and a pair of diner bandits intertwine in four tales of violence and redemption.",
                                title = "Pulp Fiction"
                            },
                            new Movie
                            {
                                released = 1996,
                                tagline =
                                    "A woman suffering from amnesia begins to recover her memories after trouble from her past finds her again.",
                                title = "The Long Kiss Goodnight"
                            },
                            new Movie
                            {
                                released = 2000,
                                tagline =
                                    " A man learns something extraordinary about himself after a devastating accident.",
                                title = "Unbreakable"
                            }
                        }
                    };

                    var parameters = new Neo4jParameters()
                        .WithEntity(actorWithMovies, "newActor")
                        .WithEntities(actorWithMovies.MoviesActedIn, "newMovies");

                    await session.RunAsync(@"
                        CREATE (person:Person $newActor)
                        WITH person
                        UNWIND $newMovies AS newMovie
                        CREATE (person)-[:ACTED_IN]->(movie:Movie)
                        SET movie = newMovie", parameters);
                }
            }
        }
    }
}
