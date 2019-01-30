using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neo4j.Driver.V1;
using Neo4jMapper;
using Queries;
using ServiceStack.Text;
using UsageGuide.Entities;
using UsageGuide.Models;

namespace UsageGuide
{
    class Program
    {
        public static async Task Main()
        {
            while (true)
            {
                ShowMenu();
                var input = Console.ReadLine()?.ToUpper();
                if (input == null) continue;

                string output = null;

                switch (input)
                {
                    case "A":
                        await LoadMovies();
                        break;
                    case "B":
                        await DeleteMovies();
                        break;

                    case "1":
                        output = await Example1();
                        break;
                    case "2":
                        output = await Example2();
                        break;
                    case "3":
                        output = await Example3();
                        break;
                    case "4":
                        output = await Example4();
                        break;
                    case "5":
                        output = await Example5();
                        break;
                    case "6":
                        output = await Example6();
                        break;
                    case "7":
                        output = await Example7();
                        break;
                    case "8":
                        output = await Example8();
                        break;
                    case "9":
                        output = await Example9();
                        break;
                    case "10":
                        output = await Example10();
                        break;
                    case "11":
                        output = await Example11();
                        break;
                    case "12":
                        output = await Example12();
                        break;
                    case "13":
                        output = await Example13();
                        break;
                    case "14":
                        output = await Example14();
                        break;
                    case "15":
                        output = await Example15();
                        break;
                }

                if (input != "Q")
                {
                    Console.WriteLine(output);
                    Console.WriteLine("Done. Press <Enter>");
                    Console.ReadLine();
                    continue;
                }

                break;
            }
        }

        private static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("Usage Guide");
            Console.WriteLine("https://www.neo4jmapper.tk/guide.html");
            Console.WriteLine("===");
            Console.WriteLine("Please select an option:");
            Console.WriteLine("A. Load Movies");
            Console.WriteLine("B. Delete Movies");
            Console.WriteLine();
            Console.WriteLine("1 - 15. Example number to run");
            Console.WriteLine();
            Console.WriteLine("Select an option or Q to Quit...");
        }

        private static async Task LoadMovies()
        {
            await Bolt.NewSession(async session =>
            {
                await session.RunAsync(Query.CreateMovies);
            });
        }

        private static async Task DeleteMovies()
        {
            await Bolt.NewSession(async session =>
            {
                await session.RunAsync(Query.DeleteMovies);
            });
        }

        static async Task<string> Example1()
        {
            return await Bolt.NewSession(session =>
            {
                var result = session.Run(@"
                    MATCH (person:Person)
                    RETURN person
                    LIMIT 1");

                var output = new StringBuilder();
                foreach (var record in result)
                {
                    output.AppendLine(record.Values.Dump());
                }

                return Task.FromResult(output.ToString());
            });
        }

        static async Task<string> Example2()
        {
            return await Bolt.NewSession(async session =>
            {
                var cursor = await session.RunAsync(@"
                    MATCH (movie:Movie)
                    RETURN movie
                    LIMIT 1");

                var output = new StringBuilder();
                foreach (var record in (await cursor.ToListAsync()))
                {
                    output.AppendLine(record.Values.Dump());
                }

                return output.ToString();
            });
        }

        static async Task<string> Example3()
        {
            return await Bolt.NewSession(async session =>
            {
                var cursor = await session.RunAsync(@"
                    MATCH (person:Person)
                    RETURN person
                    LIMIT 1");

                var person = (await cursor.SingleAsync())
                    .Map<Person>();

                return person.Dump();
            });
        }

        static async Task<string> Example4()
        {
            return await Bolt.NewSession(async session =>
            {
                var cursor = await session.RunAsync(@"
                    MATCH (actor:Person {name: 'Cuba Gooding Jr.'})-[:ACTED_IN]->(movie:Movie)
                    WITH actor, movie
                    ORDER BY movie.released
                    WITH actor, collect(movie) AS movies
                    RETURN actor, head(movies) AS firstMovie");

                var actorWithMovie = (await cursor.SingleAsync())
                    .Map((Person actor, Movie firstMovie) => new
                {
                    Actor = actor,
                    FirstMovie = firstMovie
                });

                return actorWithMovie.Dump();
            });
        }

        static async Task<string> Example5()
        {
            return await Bolt.NewSession(async session =>
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

                return actorWithMovies.Dump();
            });
        }

        class Example6Projection
        {
            public int NumberOfMovies { get; set; }
            public string ActorName { get; set; }
            public Person Actor { get; set; }
            public bool HasMovies { get; set; }
            public List<Movie> Movies { get; set; }
        }

        static async Task<string> Example6()
        {
            return await Bolt.NewSession(async session =>
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

                return actorWithMovies.Dump();
            });
        }

        class Example7CustomType
        {
            public DateTimeOffset TemporalValue { get; set; }
        }
            
        // Should fail
        static async Task<string> Example7()
        {
            return await Bolt.NewSession(async session =>
            {
                var cursor = await session.RunAsync(@"RETURN { temporalValue: datetime() } as map");

                var customType = (await cursor.SingleAsync()).Map<Example7CustomType>();

                return customType.Dump();
            });
        }

        class Example8CustomType
        {
            public ZonedDateTime TemporalValue { get; set; }
        }
            
        static async Task<string> Example8()
        {
            return await Bolt.NewSession(async session =>
            {
                var cursor = await session.RunAsync(@"RETURN { temporalValue: datetime() } as map");

                var customType = (await cursor.SingleAsync()).Map<Example8CustomType>();

                return customType.Dump();
            });
        }

        class IMDBMovie : Movie
        {
            public string imdb { get; set; }
        }

        static async Task<string> Example9()
        {
            return await Bolt.NewSession(async session =>
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

                return updatedMovie.Dump();
            });
        }

        static async Task<string> Example10()
        {
            return await Bolt.NewSession(async session =>
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

                return updatedMovie.Dump();
            });
        }

        static Task<string> Example11()
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

            return Task.FromResult(map.Dump());
        }

        static async Task<string> Example12()
        {
            return await Bolt.NewSession(async session =>
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
                    .WithEntity("newMovie", movie);

                var cursor = await session.RunAsync(@"
                    CREATE (movie:Movie $newMovie)
                    RETURN movie", parameters);

                var createdMovie = (await cursor.SingleAsync()).Map<IMDBMovie>();

                return createdMovie.Dump();
            });
        }

        static async Task<string> Example13()
        {
            return await Bolt.NewSession(async session =>
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
                    .WithEntity("newActor", actorWithMovies)
                    .WithEntities("newMovies", actorWithMovies.MoviesActedIn);

                await session.RunAsync(@"
                        CREATE (person:Person $newActor)
                        WITH person
                        UNWIND $newMovies AS newMovie
                        CREATE (person)-[:ACTED_IN]->(movie:Movie)
                        SET movie = newMovie", parameters);

                return string.Empty;
            });
        }

        static async Task<string> Example14()
        {
            return await Bolt.NewSession(async session =>
            {
                var cursor = await session.RunAsync(@"
                    MATCH (movie:Movie {released: $year})
                    RETURN movie", new {year = 1999});

                var movies = (await cursor.ToListAsync()).Map<MovieEntity>();

                var matrix = movies.Single(p => p.title == "The Matrix");
                matrix.imdb = "tt0133093";

                var updateParams = new Neo4jParameters()
                    .WithEntity("updatedMovie", matrix)
                    .WithValue("nodeId", matrix.id);

                cursor = await session.RunAsync(@"
                    MATCH (movie)
                    WHERE id(movie) = $nodeId
                    SET movie = $updatedMovie
                    RETURN movie", updateParams);

                var updatedMovie = (await cursor.SingleAsync()).Map<MovieEntity>();

                return updatedMovie.Dump();
            });
        }

        static async Task<string> Example15()
        {
            return await Bolt.NewSession(async session =>
            {
                var cursor = await session.RunAsync(@"
                    MATCH (movie:Movie {released: $year})
                    RETURN movie", new {year = 1999});

                var movies = (await cursor.ToListAsync()).Map<MovieEntity>();

                var matrix = movies.Single(p => p.title == "The Matrix");
                matrix.imdb = "tt0133093";

                await session.SetNodeAsync(matrix);

                var updatedMovie = await session.GetNodeAsync<MovieEntity>(matrix.id);

                return updatedMovie.Dump();
            });
        }
    }
}
