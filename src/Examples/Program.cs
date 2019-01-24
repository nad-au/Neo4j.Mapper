using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Examples.Entities;
using Neo4j.Driver.V1;
using Neo4jMapper;
using Queries;
using ServiceStack.Text;

namespace Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(MichelleYeohMovie).Wait();

            Console.WriteLine("Done. Press <Enter> to end.");
            Console.ReadLine();
        }

        private static async Task LoadBondMovies()
        {
            using (var driver = GraphDatabase.Driver("bolt://localhost:7687"))
            {
                using (var session = driver.Session())
                {
                    var queryParts = Query.CreateBond.Split(';');
                    foreach (var queryPart in queryParts)
                    {
                        await session.RunAsync(queryPart);
                    }
                }
            }
        }

        private static async Task DeleteBondMovies()
        {
            using (var driver = GraphDatabase.Driver("bolt://localhost:7687"))
            {
                using (var session = driver.Session())
                {
                    await session.RunAsync(Query.DeleteBond);
                }
            }
        }

        private static async Task Top3BoxOfficeMovies()
        {
            using (var driver = GraphDatabase.Driver("bolt://localhost:7687"))
            {
                using (var session = driver.Session())
                {
                    var cursor = await session.RunAsync(@"
                        MATCH (film:Film) 
                        WITH film ORDER BY film.Box DESC 
                        RETURN film 
                        LIMIT 3");

                    var films = (await cursor.ToListAsync()).Map<Film>();

                    var boxOfficeFilms = films.Select(f => new
                    {
                        f.Name,
                        f.Year,
                        BoxOffice = f.Box
                    });

                    var output = boxOfficeFilms.Dump();
                    Console.WriteLine(output);
                }
            }
        }

        private static async Task MoviesByActor()
        {
            using (var driver = GraphDatabase.Driver("bolt://localhost:7687"))
            {
                using (var session = driver.Session())
                {
                    var cursor = await session.RunAsync(@"
                        MATCH (people:People)-[:AS_BOND_IN]->(film:Film)
                        RETURN people, COLLECT(film)");

                    var actors = (await cursor.ToListAsync())
                        .Map((People people, IEnumerable<Film> films) =>
                    {
                        people.Films = films;
                        return people;
                    });

                    var moviesByActor = actors.Select(a => new
                    {
                        Actor = a.Name,
                        BondMovies = a.Films.Count()
                    });

                    var output = moviesByActor.Dump();
                    Console.WriteLine(output);
                }
            }
        }

        private static async Task MichelleYeohMovie()
        {
            using (var driver = GraphDatabase.Driver("bolt://localhost:7687"))
            {
                using (var session = driver.Session())
                {
                    var cursor = await session.RunAsync(@"
                        MATCH (people:People)-[:IS_BOND_GIRL_IN]->(film:Film) 
                        WHERE people.Name=$Name
                        RETURN people, COLLECT(film)", new
                    {
                        Name = "Michelle Yeoh"
                    });

                    var actor = (await cursor.SingleAsync())
                        .Map((People people, IEnumerable<Film> films) =>
                        {
                            people.Films = films;
                            return people;
                        });

                    var michelleFilms = actor.Films.Select(f => new
                    {
                        f.Year,
                        Title = f.Name,
                        actor.Role
                    });

                    var output = michelleFilms.Dump();
                    Console.WriteLine(output);
                }
            }
        }
    }
}
