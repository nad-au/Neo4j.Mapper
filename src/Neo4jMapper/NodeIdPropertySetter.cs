using System;
using System.Collections.Generic;
using ServiceStack;
using System.Linq;

namespace Neo4jMapper
{
    internal static class NodeIdSetter
    {
        private static readonly Dictionary<Type, INodeIdPropertySetter> CachedPropertySetters = new Dictionary<Type, INodeIdPropertySetter>();

        public static void SetNodeId<T>(this T instance, long id)
        {
            while (true)
            {
                if (CachedPropertySetters.TryGetValue(typeof(T), out var nodeIdPropertySetter))
                {
                    nodeIdPropertySetter.SetNodeId(instance, id);
                    return;
                }

                CachedPropertySetters.Add(typeof(T), new NodeIdPropertySetter<T>());
            }
        }
    }

    internal class NodeIdPropertySetter<T> : INodeIdPropertySetter
    {
        private readonly SetMemberDelegate _nodeIdPropertySetter;

        public NodeIdPropertySetter()
        {
            var allProperties = typeof(T).GetAllProperties();
            var nodeIdProperty = allProperties.SingleOrDefault(p => p.HasAttributeNamed(nameof(NodeIdAttribute)));
            if (nodeIdProperty == null) return;

            _nodeIdPropertySetter = nodeIdProperty.CreateSetter();
        }

        public void SetNodeId(object instance, long id)
        {
            _nodeIdPropertySetter?.Invoke(instance, id);
        }
    }

    internal interface INodeIdPropertySetter
    {
        void SetNodeId(object instance, long id);
    }
}
