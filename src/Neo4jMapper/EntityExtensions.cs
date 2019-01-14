using System.Collections.Generic;
using ServiceStack;

namespace Neo4jMapper
{
    public static class EntityExtensions
    {
        public static KeyValuePair<string, IReadOnlyDictionary<string, object>> ToParameter<T>(this T entity, string key) where T : class
        {
            return new KeyValuePair<string, IReadOnlyDictionary<string, object>>(key, entity.ToObjectDictionary());
        }
    }
}
