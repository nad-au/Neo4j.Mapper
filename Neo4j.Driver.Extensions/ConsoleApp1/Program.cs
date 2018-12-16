using System;
using Neo4j.Driver.Extensions;
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
                        MATCH (person:barnardos_Person)-[:PERSON_IS_CLIENT]->(client) 
                        RETURN client, person
                        LIMIT 10");

                    var clients = result.Return<Client, Person, Client>((client, person) =>
                    {
                        client.Person = person;
                        return client;
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
