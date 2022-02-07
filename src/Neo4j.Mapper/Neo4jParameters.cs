using System;
using System.Collections.Generic;

namespace Neo4j.Mapper
{
    // ReSharper disable once InconsistentNaming
    public class Neo4jParameters : Dictionary<string, object>
    {
        public Neo4jParameters(object parameters)
        {
            this.WithParams(parameters);
        }

        public Neo4jParameters()
        {
        }

        public static Func<(string key, object value), object> ValueConvert = o => o.value;
    }
}
