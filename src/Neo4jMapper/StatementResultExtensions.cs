using System;
using System.Collections.Generic;
using System.Linq;
using Neo4j.Driver.V1;

namespace Neo4jMapper
{
    public static class StatementResultExtensions
    {
        public static IEnumerable<TReturn> Return<TReturn>(
            this IStatementResult statementResult)
        {
            return statementResult.Select(record => record.Map<TReturn>());
        }

        public static IEnumerable<TReturn> Return<T1, T2, TReturn>(
            this IStatementResult statementResult,
            Func<T1, T2, TReturn> mapFunc)
        {
            return statementResult.Select(record => record.Map(mapFunc));
        }

        public static IEnumerable<TReturn> Return<T1, T2, T3, TReturn>(
            this IStatementResult statementResult,
            Func<T1, T2, T3, TReturn> mapFunc)
        {
            return statementResult.Select(record => record.Map(mapFunc));
        }
    }
}
