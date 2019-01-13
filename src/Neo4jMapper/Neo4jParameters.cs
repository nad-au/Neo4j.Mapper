using System.Collections.Generic;
using System.Reflection;
using ServiceStack;

namespace Neo4jMapper
{
    // ReSharper disable once InconsistentNaming
    public class Neo4jParameters : Dictionary<string, object>
    {
        public Neo4jParameters(object parameters)
        {
            AddParams(parameters);
        }

        public Neo4jParameters()
        {
        }

        public void Add<T>(string key, T value) where  T : class
        {
            base.Add(key, value.ToObjectDictionary());
        }

        public void AddParams(object parameters)
        {
            if (parameters == null) return;
            foreach (var propertyInfo in (parameters.GetType().GetTypeInfo().DeclaredProperties))
            {
                var key = propertyInfo.Name;
                var value = propertyInfo.GetValue(parameters);
                base.Add(key, value);
            }
        }
    }
}
