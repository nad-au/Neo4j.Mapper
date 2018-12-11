using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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
                //var result = session.Run("MATCH (p:barnardos_Person) RETURN p LIMIT 10");
                var result = session.Run("MATCH (a:Agency {Key: 'barnardos'}) RETURN a AS agency");
                var record = result.First();
                var col = record[0] as IReadOnlyDictionary<string, object>;
                var agency = col.FromObjectDictionary<Agency>();
                var processor = new ResultProcessor(record);
                processor.Define(p => p);
                var recordResult = processor.ExecuteAsNode<Person>();


                //var col = result.First()[0] as INode;
                //var p = col.Properties.FromObjectDictionary<Person>();
                //var persons = result.Select(record => (record.Values["person"].As<string>()).ToList();

                //var persons = result.Select(record => record.AsNode<Person>("p")).ToList();
                //var persons = result.Project(person => person.AsNode<Person>()).ToList();
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
