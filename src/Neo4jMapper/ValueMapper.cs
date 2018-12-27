using System.Collections;
using System.Collections.Generic;
using Neo4j.Driver.V1;
using ServiceStack;

namespace Neo4jMapper
{
    public static class ValueMapper
    {
        public static T MapValue<T>(object value)
        {
            var targetType = typeof(T);

            if (typeof(IEnumerable).IsAssignableFrom(targetType))
            {
                if (targetType == typeof(string))
                {
                    return value.As<T>();
                }

                var elementType = targetType.GetGenericArguments()[0];
                var genericType = typeof(CollectionMapper<>).MakeGenericType(elementType);
                var collectionMapper = (ICollectionMapper)genericType.CreateInstance();
                return (T)collectionMapper.MapValues((IEnumerable)value, targetType);
            }

            if (value is INode node)
            {
                return node.Properties.FromObjectDictionary<T>();
            }

            if (value is IReadOnlyDictionary<string, object> map)
            {
                return map.FromObjectDictionary<T>();
            }

            return value.As<T>();
        }
    }
}
