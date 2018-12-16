using System;
using System.Collections.Generic;
using Neo4j.Driver.V1;

namespace ConsoleApp1
{
    public static class StatementResultExtensions
    {
        public static IEnumerable<TReturn> Return<T1, T2, TReturn>(
            this IStatementResult statementResult,
            Func<T1, T2, TReturn> mapFunc)
        {
            foreach (var record in statementResult)
            {
                yield return record.Map(mapFunc);
            }
        }
    }
}
