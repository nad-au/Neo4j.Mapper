﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ServiceStack;

namespace Neo4jMapper
{
    public static class DictionaryExtensions
    {
        public static IDictionary<string, object> WithEntity<T>(this IDictionary<string, object> dictionary, T entity, string key) where  T : class
        {
            dictionary.WithMap(entity.ToParameterMap(key));

            return dictionary;
        }

        public static IDictionary<string, object> WithEntities<T>(this IDictionary<string, object> dictionary, IEnumerable<T> entities, string key) where  T : class
        {
            dictionary.WithMaps(entities.ToParameterMaps(key));

            return dictionary;
        }

        public static IDictionary<string, object> WithMap(this IDictionary<string, object> dictionary, KeyValuePair<string, IReadOnlyDictionary<string, object>> kvp)
        {
            dictionary.Add(kvp.Key, kvp.Value);

            return dictionary;
        }

        public static IDictionary<string, object> WithMaps(this IDictionary<string, object> dictionary, KeyValuePair<string, IEnumerable<IReadOnlyDictionary<string, object>>> kvp)
        {
            dictionary.Add(kvp.Key, kvp.Value);

            return dictionary;
        }

        public static IDictionary<string, object> WithParams(this IDictionary<string, object> dictionary, object parameters)
        {
            if (parameters == null) return dictionary;

            foreach (var propertyInfo in (parameters.GetType().GetTypeInfo().DeclaredProperties))
            {
                var key = propertyInfo.Name;
                var value = propertyInfo.GetValue(parameters);
                dictionary.Add(key, value);
            }

            return dictionary;
        }

        public static IDictionary<string, object> WithValue(this IDictionary<string, object> dictionary, string key, object value)
        {
            dictionary.Add(key, value);

            return dictionary;
        }
    }
}
