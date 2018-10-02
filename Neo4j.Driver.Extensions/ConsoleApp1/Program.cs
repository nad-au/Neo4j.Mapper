using System.Linq;
using Neo4j.Driver.V1;
using ServiceStack;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var driver = GraphDatabase.Driver("bolt://localhost:7687");
            using(var session = driver.Session())
            {
                var result = session.Run("MATCH (p:barnardos_Person) RETURN p LIMIT 10");
                var persons = result.Select(record => record.MapFromNode<Person>("p")).ToList();
            }
            driver.Dispose();
        }
    }
}
