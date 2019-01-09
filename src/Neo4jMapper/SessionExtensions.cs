using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Neo4j.Driver.V1;
using ServiceStack;

namespace Neo4jMapper
{
    public static class SessionExtensions
    {
        public static IStatementResult Run<T>(
            this ISession session,
            string statement,
            Expression<Func<T, object>> expression)
        {
            var memberSelector = (MemberExpression)expression.Body;
            var constantSelector = (ConstantExpression)memberSelector.Expression;

            var paramName = expression.Parameters[0].Name;
            var value = ((FieldInfo)memberSelector.Member)
                .GetValue(constantSelector.Value);
            
            var parameters = new Dictionary<string, object>
            {
                { paramName, value.ToObjectDictionary() }
            };

            return session.Run(statement, parameters);
        }

        public static IStatementResult UpdateNode<T>(
            this ISession session,
            T entity) where T : class
        {
            const string statement = @"
                MATCH (node)
                WHERE id(node) = $p1
                SET node = $p2";

            var nodeId = entity.GetNodeId();
            if (nodeId == null)
                throw new Exception("NodeIdAttribute not specified or the Node Id is null");

            return session.Run(statement, nodeId.Value);
        }
    }
}
