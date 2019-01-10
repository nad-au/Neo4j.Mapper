using System;
using System.Collections;
using System.Collections.Generic;
using Neo4j.Driver.V1;
using ServiceStack;

namespace Neo4jMapper
{
    public static class ValueMapper
    {
        public static T MapValue<T>(object cypherValue)
        {
            var targetType = typeof(T);

            if (typeof(IEnumerable).IsAssignableFrom(targetType))
            {
                if (!(cypherValue is IEnumerable enumerable))
                    throw new ArgumentException($"The cypher value is not a list and cannot be mapped to target type: {targetType.FullName}");

                if (targetType == typeof(string))
                {
                    return enumerable.As<T>();
                }

                var elementType = targetType.GetGenericArguments()[0];
                var genericType = typeof(CollectionMapper<>).MakeGenericType(elementType);
                var collectionMapper = (ICollectionMapper)genericType.CreateInstance();
                
                return (T)collectionMapper.MapValues(enumerable, targetType);
            }

            if (cypherValue is INode node)
            {
                var entity = node.Properties.FromObjectDictionary<T>();
                EntityAccessor.SetNodeId(entity, node.Id);

                return entity;
            }

            if (cypherValue is IReadOnlyDictionary<string, object> map)
            {
                return map.FromObjectDictionary<T>();
            }

            if (cypherValue is IEnumerable)
                throw new ArgumentException($"The cypher value is a list and cannot be mapped to target type: {targetType.FullName}");

            return cypherValue.As<T>();
        }
    }
}
