using System.Linq;
using Neo4j.Driver.Extensions.Tests.Models;
using Neo4j.Driver.V1;
using NUnit.Framework;

namespace Neo4j.Driver.Extensions.Tests
{
    [TestFixture]
    public class BasicTests
    {
        [Test]
        public void ShouldWorkAsExpected()
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
                    }).ToList();

                    Assert.AreEqual(10, clients.Count);
                }
            }

        }
    }
}
