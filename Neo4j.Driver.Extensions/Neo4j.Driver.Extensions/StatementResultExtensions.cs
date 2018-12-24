using System;
using System.Collections.Generic;
using Neo4j.Driver.V1;

namespace Neo4j.Driver.Extensions
{
    public static class StatementResultExtensions
    {
        public static IEnumerable<TReturn> Return<TReturn>(
            this IStatementResult statementResult)
        {
            foreach (var record in statementResult)
            {
                yield return record.Map<TReturn>();
            }
        }

        public static IEnumerable<TReturn> Return<T1, T2, TReturn>(
            this IStatementResult statementResult,
            Func<T1, T2, TReturn> mapFunc)
        {
            foreach (var record in statementResult)
            {
                yield return record.Map(mapFunc);
            }
        }

        public static IEnumerable<TReturn> Return<T1, T2, T3, TReturn>(
            this IStatementResult statementResult,
            Func<T1, T2, T3, TReturn> mapFunc)
        {
            foreach (var record in statementResult)
            {
                yield return record.Map(mapFunc);
            }
        }
    }
}
