using System;
using System.Collections.Concurrent;
using ServiceStack;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("UnitTests")]
namespace Neo4jMapper
{
    internal static class EntityAccessor
    {
        private static readonly ConcurrentDictionary<Type, IEntityPropertyAccessor> CachedPropertyAccessors = new ConcurrentDictionary<Type, IEntityPropertyAccessor>();

        public static long? GetNodeId<T>(T entity)
        {
            return CachedPropertyAccessors
                .GetOrAdd(typeof(T), new EntityPropertyAccessor<T>())
                .GetNodeId(entity);
        }

        public static void SetNodeId<T>(T entity, long id)
        {
            CachedPropertyAccessors
                .GetOrAdd(typeof(T), new EntityPropertyAccessor<T>())
                .SetNodeId(entity, id);
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
