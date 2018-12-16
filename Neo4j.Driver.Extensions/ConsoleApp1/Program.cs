using System;
using Neo4j.Driver.V1;
using ServiceStack.Text;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var driver = GraphDatabase.Driver("bolt://localhost:7687"))
            {
                using(var session = driver.Session())
                {
                    var result = session.Run(@"
                        MATCH (p:barnardos_Person)-[:PERSON_IS_CLIENT]->(c) 
                        RETURN c, p
                        LIMIT 10");

                    var clients = result.Return<Client, Person, Client>((c, p) =>
                    {
                        c.Person = p;
                        return c;
                    });

                    foreach (var client in clients)
                    {
                        Console.WriteLine(client.Dump());
                    }
                }
            }

            Console.WriteLine("Done. Press <Enter> to end");
            Console.ReadLine();
        }
    }
}
