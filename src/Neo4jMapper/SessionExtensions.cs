using System;
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

            var parameters = new Neo4jParameters
            {
                {paramName, value.ToObjectDictionary()}
            };

            return session.Run(statement, parameters);
        }

        public static TEntity GetNode<TEntity>(
            this ISession session,
            long nodeId) where TEntity : class
        {
            const string statement = @"
                MATCH (node)
                WHERE id(node) = $p1
                RETURN node";

            var parameters = new Neo4jParameters(new
            {
                p1 = nodeId
            });

            return session
                .Run(statement, parameters)
                .Return<TEntity>()
                .SingleOrDefault();
        }

        public static IStatementResult UpdateNode<TEntity>(
            this ISession session,
            TEntity entity) where TEntity : class
        {
            const string statement = @"
                MATCH (node)
                WHERE id(node) = $p1
                SET node = $p2";

            var nodeId = EntityAccessor.GetNodeId(entity);
            if (nodeId == null)
                throw new Exception("NodeIdAttribute not specified or the Node Id is null");

            var parameters = new Neo4jParameters(new
            {
                p1 = nodeId,
                p2 = entity.ToObjectDictionary()
            });

            return session.Run(statement, parameters);
        }
    }
}
