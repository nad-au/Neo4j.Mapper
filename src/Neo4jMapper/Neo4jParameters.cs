using System.Collections.Generic;

namespace Neo4jMapper
{
    // ReSharper disable once InconsistentNaming
    public class Neo4jParameters : Dictionary<string, object>
    {
        public Neo4jParameters(object parameters)
        {
            this.AddParams(parameters);
        }

        public Neo4jParameters()
        {
        }
    }
}
