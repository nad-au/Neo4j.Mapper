using System;
using System.Collections.Generic;
using System.Linq;
using Neo4j.Driver.V1;
using ServiceStack;

namespace Neo4j.Driver.Extensions
{
    public class Class1
    {
        public void Test()
        {
            var driver = GraphDatabase.Driver("bolt://localhost:7687");
            using(var session = driver.Session())
            {
                var result = session.Run("MATCH (a:Agency) RETURN a.name as name");
                var agencies = result.Select(record => record.Values.FromObjectDictionary<Agency>()).ToList();
            }
            driver.Dispose();
        }
    }

    public class Agency
    {
        public string Key { get; set; }
        public string Name { get; set; }
    }
}
