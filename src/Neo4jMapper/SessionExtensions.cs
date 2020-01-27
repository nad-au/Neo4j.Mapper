using System;
using System.Linq;
using Neo4j.Driver;

namespace Neo4jMapper
{
    public static class SessionExtensions
    {
        public static TEntity GetNode<TEntity>(
            this ISession session,
            long nodeId) where TEntity : class
        {
            var parameters = new Neo4jParameters()
                .WithValue("p1", nodeId);

            return session
                .Run(Constants.Statement.GetNode, parameters)
                .Map<TEntity>()
                .SingleOrDefault();
        }

        public static TEntity GetNode<TEntity>(
            this ITransaction transaction,
            long nodeId) where TEntity : class
        {
            var parameters = new Neo4jParameters()
                .WithValue("p1", nodeId);

            return transaction
                .Run(Constants.Statement.GetNode, parameters)
                .Map<TEntity>()
                .SingleOrDefault();
        }

        public static IResult SetNode<TEntity>(
            this ISession session,
            TEntity entity) where TEntity : class
        {
            var nodeId = EntityAccessor.GetNodeId(entity);
            if (nodeId == null)
                throw new InvalidOperationException(Constants.NodeIdUnspecifiedMessage);

            var parameters = new Neo4jParameters()
                .WithValue("p1", nodeId)
                .WithEntity("p2", entity);

            return session.Run(Constants.Statement.SetNode, parameters);
        }

        public static IResult SetNode<TEntity>(
            this ITransaction transaction,
            TEntity entity) where TEntity : class
        {
            var nodeId = EntityAccessor.GetNodeId(entity);
            if (nodeId == null)
                throw new InvalidOperationException(Constants.NodeIdUnspecifiedMessage);

            var parameters = new Neo4jParameters()
                .WithValue("p1", nodeId)
                .WithEntity("p2", entity);

            return transaction.Run(Constants.Statement.SetNode, parameters);
        }
    }
}
