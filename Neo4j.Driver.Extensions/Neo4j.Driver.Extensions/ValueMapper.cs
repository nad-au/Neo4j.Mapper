using System;
using System.Collections;
using System.Collections.Generic;
using Neo4j.Driver.V1;
using ServiceStack;

namespace Neo4j.Driver.Extensions
{
    public static class ValueMapper
    {
        public static T MapValue<T>(object value)
        {
            return (T) MapValue(value, typeof(T));
        }

        public static object MapValue(object value, Type type)
        {
            if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                var elementType = type.GetGenericArguments()[0];
                var genericType = typeof(CollectionMapper<>).MakeGenericType(elementType);
                var collectionMapper = (ICollectionMapper)genericType.CreateInstance();
                return collectionMapper.MapValues((IEnumerable)value, type);
            }

            if (value is INode node)
            {
                return node.Properties.FromObjectDictionary(type);
            }

            if (value is IReadOnlyDictionary<string, object> map)
            {
                return map.FromObjectDictionary(type);
            }

            throw new ArgumentException("Cannot deserialize value");
        }
    }
}
