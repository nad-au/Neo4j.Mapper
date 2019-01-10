using System;
using System.Collections.Generic;
using ServiceStack;
using System.Linq;

namespace Neo4jMapper
{
    internal static class EntityAccessor
    {
        private static readonly Dictionary<Type, IEntityPropertyAccessor> CachedPropertyAccessors = new Dictionary<Type, IEntityPropertyAccessor>();

        public static long? GetNodeId<T>(T entity)
        {
            while (true)
            {
                if (CachedPropertyAccessors.TryGetValue(typeof(T), out var entityPropertyAccessor))
                {
                    return entityPropertyAccessor.GetNodeId(entity);
                }

                CachedPropertyAccessors.Add(typeof(T), new EntityPropertyAccessor<T>());
            }
        }

        public static void SetNodeId<T>(T entity, long id)
        {
            while (true)
            {
                if (CachedPropertyAccessors.TryGetValue(typeof(T), out var entityPropertyAccessor))
                {
                    entityPropertyAccessor.SetNodeId(entity, id);
                    return;
                }

                CachedPropertyAccessors.Add(typeof(T), new EntityPropertyAccessor<T>());
            }
        }
    }

    internal class EntityPropertyAccessor<T> : IEntityPropertyAccessor
    {
        private readonly SetMemberDelegate _nodeIdPropertySetter;
        private readonly GetMemberDelegate _nodeIdPropertyGetter;

        public EntityPropertyAccessor()
        {
            var allProperties = typeof(T).GetAllProperties();
            var nodeIdProperty = allProperties.SingleOrDefault(p => p.HasAttributeNamed(nameof(NodeIdAttribute)));
            if (nodeIdProperty == null) return;

            _nodeIdPropertySetter = nodeIdProperty.CreateSetter();
            _nodeIdPropertyGetter = nodeIdProperty.CreateGetter();
        }

        public long? GetNodeId(object instance)
        {
            return (long?) _nodeIdPropertyGetter?.Invoke(instance);
        }

        public void SetNodeId(object instance, long id)
        {
            _nodeIdPropertySetter?.Invoke(instance, id);
        }
    }

    internal interface IEntityPropertyAccessor
    {
        void SetNodeId(object instance, long id);
        long? GetNodeId(object instance);
    }
}
