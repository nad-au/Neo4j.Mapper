﻿using System.Collections.Generic;
using System.Linq;
using ServiceStack;

namespace Neo4jMapper
{
    public static class EntityExtensions
    {
        public static KeyValuePair<string, IReadOnlyDictionary<string, object>> ToParameterMap<T>(this T entity, string key) where T : class
        {
            var map = entity.ToObjectDictionary();
            return new KeyValuePair<string, IReadOnlyDictionary<string, object>>(key, map);
        }

        public static KeyValuePair<string, IEnumerable<IReadOnlyDictionary<string, object>>> ToParameterMaps<T>(this IEnumerable<T> entities, string key) where T : class
        {
            var map = entities.Select(p => p.ToObjectDictionary());
            return new KeyValuePair<string, IEnumerable<IReadOnlyDictionary<string, object>>>(key, map);
        }
    }
}
