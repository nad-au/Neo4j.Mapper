using System;
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
                if (!(value is IEnumerable))
                    throw new ArgumentException("Expecting a collection but the cypher value is not a list");

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
                var entity = node.Properties.FromObjectDictionary<T>();
                entity.SetNodeId(node.Id);

                return entity;
            }

            if (value is IReadOnlyDictionary<string, object> map)
            {
                return map.FromObjectDictionary<T>();
            }

            if (value is IEnumerable)
                throw new ArgumentException("Not expecting a collection but the cypher value is a list");

            return value.As<T>();
        }
    }
}
