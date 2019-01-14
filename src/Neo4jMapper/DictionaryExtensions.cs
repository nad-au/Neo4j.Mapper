using System.Collections.Generic;
using System.Reflection;

namespace Neo4jMapper
{
    public static class DictionaryExtensions
    {
        public static IDictionary<string, object> AddEntity<T>(this IDictionary<string, object> dictionary, string key, T entity) where  T : class
        {
            dictionary.AddMap(entity.ToParameter(key));

            return dictionary;
        }

        public static IDictionary<string, object> AddMap(this IDictionary<string, object> dictionary, KeyValuePair<string, IReadOnlyDictionary<string, object>> kvp)
        {
            dictionary.Add(kvp.Key, kvp.Value);

            return dictionary;
        }

        public static IDictionary<string, object> AddParams(this IDictionary<string, object> dictionary, object parameters)
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
    }
}
