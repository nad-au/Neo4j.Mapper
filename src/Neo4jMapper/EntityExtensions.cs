using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack;

namespace Neo4jMapper
{
    public static class EntityExtensions
    {
        public static KeyValuePair<string, IReadOnlyDictionary<string, object>> ToParameterMap<T>(
            this T entity,
            string key,
            Func<string, object, object> mapper = null) where T : class
        {
            var map = entity.ToObjectDictionary();
            if(mapper != null)
                map= map.ToDictionary(p => p.Key, p => mapper(p.Key, p.Value));
            return new KeyValuePair<string, IReadOnlyDictionary<string, object>>(key, map);
        }

        public static KeyValuePair<string, IEnumerable<IReadOnlyDictionary<string, object>>> ToParameterMaps<T>(
            this IEnumerable<T> entities,
            string key,
            Func<string, object, object> mapper = null) where T : class
        {
            var map = entities.Select(p =>
            {
                var map = p.ToObjectDictionary();
                if (mapper != null)
                    map = map.ToDictionary(p => p.Key, p => mapper(p.Key, p.Value));
                return map;
            });
            return new KeyValuePair<string, IEnumerable<IReadOnlyDictionary<string, object>>>(key, map);
        }
    }
}
