using System;
using System.Linq;
using System.Threading.Tasks;
using Neo4j.Driver.V1;

namespace Neo4jMapper
{
    public static class SessionExtensions
    {
        private const string GetNodeStatement = @"
                MATCH (node)
                WHERE id(node) = $p1
                RETURN node";
        
        private const string SetNodeStatement = @"
                MATCH (node)
                WHERE id(node) = $p1
                SET node = $p2";

        private static readonly string NodeIdUnspecifiedMessage = $"{nameof(NodeIdAttribute)} not specified or the Node Id is null";

        public static TEntity GetNode<TEntity>(
            this ISession session,
            long nodeId) where TEntity : class
        {
            var parameters = new Neo4jParameters()
                .WithValue("p1", nodeId);

            return session
                .Run(GetNodeStatement, parameters)
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
                .Run(GetNodeStatement, parameters)
                .Map<TEntity>()
                .SingleOrDefault();
        }

        public static async Task<TEntity> GetNodeAsync<TEntity>(
            this ISession session,
            long nodeId) where TEntity : class
        {
            var parameters = new Neo4jParameters()
                .WithValue("p1", nodeId);

            var statementResultCursor = await session.RunAsync(GetNodeStatement, parameters);

            return await statementResultCursor.MapSingleAsync<TEntity>();
        }

        public static async Task<TEntity> GetNodeAsync<TEntity>(
            this ITransaction transaction,
            long nodeId) where TEntity : class
        {
            var parameters = new Neo4jParameters()
                .WithValue("p1", nodeId);

            var statementResultCursor = await transaction.RunAsync(GetNodeStatement, parameters);

            return await statementResultCursor.MapSingleAsync<TEntity>();
        }

        public static IStatementResult SetNode<TEntity>(
            this ISession session,
            TEntity entity) where TEntity : class
        {
            var nodeId = EntityAccessor.GetNodeId(entity);
            if (nodeId == null)
                throw new InvalidOperationException(NodeIdUnspecifiedMessage);

            var parameters = new Neo4jParameters()
                .WithValue("p1", nodeId)
                .WithEntity("p2", entity);

            return session.Run(SetNodeStatement, parameters);
        }

        public static IStatementResult SetNode<TEntity>(
            this ITransaction transaction,
            TEntity entity) where TEntity : class
        {
            var nodeId = EntityAccessor.GetNodeId(entity);
            if (nodeId == null)
                throw new InvalidOperationException(NodeIdUnspecifiedMessage);

            var parameters = new Neo4jParameters()
                .WithValue("p1", nodeId)
                .WithEntity("p2", entity);

            return transaction.Run(SetNodeStatement, parameters);
        }

        public static async Task<IStatementResultCursor> SetNodeAsync<TEntity>(
            this ISession session,
            TEntity entity) where TEntity : class
        {
            var nodeId = EntityAccessor.GetNodeId(entity);
            if (nodeId == null)
                throw new InvalidOperationException(NodeIdUnspecifiedMessage);

            var parameters = new Neo4jParameters()
                .WithValue("p1", nodeId)
                .WithEntity("p2", entity);

            return await session.RunAsync(SetNodeStatement, parameters);
        }

        public static async Task<IStatementResultCursor> SetNodeAsync<TEntity>(
            this ITransaction transaction,
            TEntity entity) where TEntity : class
        {
            var nodeId = EntityAccessor.GetNodeId(entity);
            if (nodeId == null)
                throw new InvalidOperationException(NodeIdUnspecifiedMessage);

            var parameters = new Neo4jParameters()
                .WithValue("p1", nodeId)
                .WithEntity("p2", entity);

            return await transaction.RunAsync(SetNodeStatement, parameters);
        }
    }
}
