﻿using System;
using System.Collections;
using System.Collections.Generic;
using Neo4j.Driver;
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
                    throw new InvalidOperationException($"The cypher value is not a list and cannot be mapped to target type: {targetType.UnderlyingSystemType}");

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

            if (cypherValue is IRelationship relationship)
            {
                var entity = relationship.Properties.FromObjectDictionary<T>();

                return entity;
            }

            if (cypherValue is IReadOnlyDictionary<string, object> map)
            {
                return map.FromObjectDictionary<T>();
            }

            if (cypherValue is IEnumerable)
                throw new InvalidOperationException($"The cypher value is a list and cannot be mapped to target type: {targetType.UnderlyingSystemType}");

            return cypherValue.As<T>();
        }
    }
}
