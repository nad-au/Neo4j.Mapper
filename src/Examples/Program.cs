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
    public class Program
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
                        await LoadBondMovies();
                        break;
                    case "B":
                        await DeleteBondMovies();
                        break;

                    case "1":
                        output = await Top3BoxOfficeMovies();
                        break;
                    case "2":
                        output = await MoviesByActor();
                        break;
                    case "3":
                        output = await MichelleYeohMovie();
                        break;
                    case "4":
                        output = await VehicleBrands();
                        break;
                    case "5":
                        output = await Directors();
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
            Console.WriteLine("GraphGist: Know more about James Bond Movie");
            Console.WriteLine("by Eric Lee - https://neo4j.com/graphgist/know-more-about-james-bond-movie");
            Console.WriteLine("===");
            Console.WriteLine("Please select an option:");
            Console.WriteLine("A. Load Bond Movies");
            Console.WriteLine("B. Delete Bond Movies");
            Console.WriteLine();
            Console.WriteLine("1. Top 3 Box Office Movies");
            Console.WriteLine("2. Movies By Actor");
            Console.WriteLine("3. Michelle Yeoh Movie");
            Console.WriteLine("4. Vehicle Brands");
            Console.WriteLine("5. Directors");
            Console.WriteLine();
            Console.WriteLine("Select an option or Q to Quit...");
        }

        private static async Task LoadBondMovies()
        {
            await Bolt.NewSession(async session =>
            {
                var queryParts = Query.CreateBond.Split(';');
                foreach (var queryPart in queryParts)
                {
                    await session.RunAsync(queryPart);
                }
            });
        }

        private static async Task DeleteBondMovies()
        {
            await Bolt.NewSession(async session =>
            {
                await session.RunAsync(Query.DeleteBond);
            });
        }

        private static async Task<string> Top3BoxOfficeMovies()
        {
            return await Bolt.NewSession(async session =>
            {
                var cursor = await session.RunAsync(@"
                    MATCH (film:Film) 
                    WITH film ORDER BY film.Box DESC 
                    RETURN film 
                    LIMIT 3");

                var boxOfficeFilms = (await cursor.ToListAsync())
                    .Map<Film>()
                    .Select(f => new
                    {
                        f.Name,
                        f.Year,
                        BoxOffice = f.Box
                    });

                return boxOfficeFilms.Dump();
            });
        }

        private static async Task<string> MoviesByActor()
        {
            return await Bolt.NewSession(async session =>
            {
                var cursor = await session.RunAsync(@"
                    MATCH (people:People)-[:AS_BOND_IN]->(film:Film)
                    RETURN people, COLLECT(film)");

                var moviesByActor = (await cursor.ToListAsync())
                    .Map((People people, IEnumerable<Film> films) => new
                    {
                        Actor = people.Name,
                        BondMovies = films.Count()
                    }).OrderByDescending(o => o.BondMovies);

                return moviesByActor.Dump();
            });
        }

        private static async Task<string> MichelleYeohMovie()
        {
            return await Bolt.NewSession(async session =>
            {
                var cursor = await session.RunAsync(@"
                    MATCH (people:People)-[:IS_BOND_GIRL_IN]->(film:Film) 
                    WHERE people.Name=$Name
                    RETURN people, film", new
                {
                    Name = "Michelle Yeoh"
                });

                var michelleFilms = (await cursor.ToListAsync())
                    .Map((People people, Film film) => new
                    {
                        film.Year,
                        Title = film.Name,
                        people.Role
                    });

                return michelleFilms.Dump();
            });
        }

        private static async Task<string> VehicleBrands()
        {
            return await Bolt.NewSession(async session =>
            {
                var cursor = await session.RunAsync(@"
                    MATCH (film:Film)-[:HAS_VEHICLE]->(vehicle:Vehicle)
                    RETURN DISTINCT vehicle.Brand AS Brand, 
                    count(vehicle.Model) AS Models, 
                    collect(DISTINCT film) AS Films");

                var vehicleBrands = (await cursor.ToListAsync())
                    .Map((string brand, int modelCount, IEnumerable<Film> films) => new
                    {
                        Brand = brand,
                        Models = modelCount,
                        Movies = films.Select(f => f.Name)
                    }).OrderByDescending(o => o.Models);

                return vehicleBrands.Dump();
            });
        }

        private static async Task<string> Directors()
        {
            return await Bolt.NewSession(async session =>
            {
                var cursor = await session.RunAsync(@"
                    MATCH (people:People)-[r:DIRECTOR_OF]->(film:Film)
                    RETURN people, COLLECT(film)");

                var directedMovies = (await cursor.ToListAsync())
                    .Map((People people, IEnumerable<Film> films) => new
                    {
                        Director = people.Name,
                        Time = films.Count()
                    }).OrderByDescending(o => o.Time);

                return directedMovies.Dump();
            });
        }
    }
}
