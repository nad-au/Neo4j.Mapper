using System.Linq;
using Neo4j.Driver.V1;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var driver = GraphDatabase.Driver("bolt://localhost:7687");
            using(var session = driver.Session())
            {
                var result = session.Run("MATCH (p:barnardos_Person) RETURN p as person, p.GivenName AS givenName LIMIT 10");
                //var persons = result.Select(record => record.AsNode<Person>("p")).ToList();
                var persons = result.Project(person => person.AsNode<Person>()).ToList();
                //var names = result.Project(givenName => givenName.As<string>()).ToList();
                //var items = result.Project((person, givenName) => new
                //{
                //    Person = person.AsNode<Person>(),
                //    GivenName = givenName.As<string>()
                //}).ToList();
            }
            driver.Dispose();
        }
    }
}
