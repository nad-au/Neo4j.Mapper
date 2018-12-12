using System;
using System.Linq;
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
                        MATCH (p:barnardos_Person {UniqueId: 10233})-[:PERSON_IS_CLIENT]->(c) 
                        RETURN c.StateJurisdiction, p");

                    var record = result.First();

                    var client = record.Map<string, Person, Client>((s, p) => new Client
                    {
                        StateJurisdiction = s,
                        Person = p
                    });

                    Console.WriteLine(client.Dump());
                }
            }

            Console.WriteLine("Done. Press <Enter> to end");
            Console.ReadLine();
        }
    }
}
