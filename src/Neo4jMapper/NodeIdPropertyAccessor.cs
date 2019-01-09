using System;
using System.Collections.Generic;
using ServiceStack;
using System.Linq;

namespace Neo4jMapper
{
    internal static class NodeIdAccessor
    {
        private static readonly Dictionary<Type, INodeIdPropertyAccessor> CachedPropertyAccessors = new Dictionary<Type, INodeIdPropertyAccessor>();

        public static long? GetNodeId<T>(this T instance)
        {
            while (true)
            {
                if (CachedPropertyAccessors.TryGetValue(typeof(T), out var nodeIdPropertyAccessor))
                {
                    return nodeIdPropertyAccessor.GetNodeId(instance);
                }

                CachedPropertyAccessors.Add(typeof(T), new NodeIdPropertyAccessor<T>());
            }
        }

        public static void SetNodeId<T>(this T instance, long id)
        {
            while (true)
            {
                if (CachedPropertyAccessors.TryGetValue(typeof(T), out var nodeIdPropertyAccessor))
                {
                    nodeIdPropertyAccessor.SetNodeId(instance, id);
                    return;
                }

                CachedPropertyAccessors.Add(typeof(T), new NodeIdPropertyAccessor<T>());
            }
        }
    }

    internal class NodeIdPropertyAccessor<T> : INodeIdPropertyAccessor
    {
        private readonly SetMemberDelegate nodeIdPropertySetter;
        private readonly GetMemberDelegate nodeIdPropertyGetter;

        public NodeIdPropertyAccessor()
        {
            var allProperties = typeof(T).GetAllProperties();
            var nodeIdProperty = allProperties.SingleOrDefault(p => p.HasAttributeNamed(nameof(NodeIdAttribute)));
            if (nodeIdProperty == null) return;

            nodeIdPropertySetter = nodeIdProperty.CreateSetter();
            nodeIdPropertyGetter = nodeIdProperty.CreateGetter();
        }

        public long? GetNodeId(object instance)
        {
            return (long?) nodeIdPropertyGetter?.Invoke(instance);
        }

        public void SetNodeId(object instance, long id)
        {
            nodeIdPropertySetter?.Invoke(instance, id);
        }
    }

    internal interface INodeIdPropertyAccessor
    {
        void SetNodeId(object instance, long id);
        long? GetNodeId(object instance);
    }
}
