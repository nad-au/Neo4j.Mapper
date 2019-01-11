using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Neo4j.Driver.V1;

namespace Neo4jMapper
{
    public static class StatementResultCursorExtensions
    {
        public static async Task<IEnumerable<TReturn>> MapAsync<TReturn>(
            this IStatementResultCursor statementResultCursor)
        {
            return (await statementResultCursor.ToListAsync()).Map<TReturn>();
        }

        public static async Task<IEnumerable<TReturn>> MapAsync<TValue1, TValue2, TReturn>(
            this IStatementResultCursor statementResultCursor,
            Func<TValue1, TValue2, TReturn> mapFunc)
        {
            return (await statementResultCursor.ToListAsync()).Map(mapFunc);
        }

        public static async Task<IEnumerable<TReturn>> MapAsync<TValue1, TValue2, TValue3, TReturn>(
            this IStatementResultCursor statementResultCursor,
            Func<TValue1, TValue2, TValue3, TReturn> mapFunc)
        {
            return (await statementResultCursor.ToListAsync()).Map(mapFunc);
        }

        public static async Task<IEnumerable<TReturn>> MapAsync<TValue1, TValue2, TValue3, TValue4, TReturn>(
            this IStatementResultCursor statementResultCursor,
            Func<TValue1, TValue2, TValue3, TValue4, TReturn> mapFunc)
        {
            return (await statementResultCursor.ToListAsync()).Map(mapFunc);
        }

        public static async Task<IEnumerable<TReturn>> MapAsync<TValue1, TValue2, TValue3, TValue4, TValue5, TReturn>(
            this IStatementResultCursor statementResultCursor,
            Func<TValue1, TValue2, TValue3, TValue4, TValue5, TReturn> mapFunc)
        {
            return (await statementResultCursor.ToListAsync()).Map(mapFunc);
        }

        public static async Task<IEnumerable<TReturn>> MapAsync<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TReturn>(
            this IStatementResultCursor statementResultCursor,
            Func<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TReturn> mapFunc)
        {
            return (await statementResultCursor.ToListAsync()).Map(mapFunc);
        }

        public static async Task<IEnumerable<TReturn>> MapAsync<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7, TReturn>(
            this IStatementResultCursor statementResultCursor,
            Func<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7, TReturn> mapFunc)
        {
            return (await statementResultCursor.ToListAsync()).Map(mapFunc);
        }

        public static async Task<IEnumerable<TReturn>> MapAsync<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7, TValue8, TReturn>(
            this IStatementResultCursor statementResultCursor,
            Func<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7, TValue8, TReturn> mapFunc)
        {
            return (await statementResultCursor.ToListAsync()).Map(mapFunc);
        }

        public static async Task<IEnumerable<TReturn>> MapAsync<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7, TValue8, TValue9, TReturn>(
            this IStatementResultCursor statementResultCursor,
            Func<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7, TValue8, TValue9, TReturn> mapFunc)
        {
            return (await statementResultCursor.ToListAsync()).Map(mapFunc);
        }

        public static async Task<IEnumerable<TReturn>> MapAsync<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7, TValue8, TValue9, TValue10, TReturn>(
            this IStatementResultCursor statementResultCursor,
            Func<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7, TValue8, TValue9, TValue10, TReturn> mapFunc)
        {
            return (await statementResultCursor.ToListAsync()).Map(mapFunc);
        }

        public static async Task<IEnumerable<TReturn>> MapAsync<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7, TValue8, TValue9, TValue10, TValue11, TReturn>(
            this IStatementResultCursor statementResultCursor,
            Func<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7, TValue8, TValue9, TValue10, TValue11, TReturn> mapFunc)
        {
            return (await statementResultCursor.ToListAsync()).Map(mapFunc);
        }

        public static async Task<IEnumerable<TReturn>> MapAsync<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7, TValue8, TValue9, TValue10, TValue11, TValue12, TReturn>(
            this IStatementResultCursor statementResultCursor,
            Func<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7, TValue8, TValue9, TValue10, TValue11, TValue12, TReturn> mapFunc)
        {
            return (await statementResultCursor.ToListAsync()).Map(mapFunc);
        }

        public static async Task<IEnumerable<TReturn>> MapAsync<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7, TValue8, TValue9, TValue10, TValue11, TValue12, TValue13, TReturn>(
            this IStatementResultCursor statementResultCursor,
            Func<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7, TValue8, TValue9, TValue10, TValue11, TValue12, TValue13, TReturn> mapFunc)
        {
            return (await statementResultCursor.ToListAsync()).Map(mapFunc);
        }

        public static async Task<IEnumerable<TReturn>> MapAsync<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7, TValue8, TValue9, TValue10, TValue11, TValue12, TValue13, TValue14, TReturn>(
            this IStatementResultCursor statementResultCursor,
            Func<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7, TValue8, TValue9, TValue10, TValue11, TValue12, TValue13, TValue14, TReturn> mapFunc)
        {
            return (await statementResultCursor.ToListAsync()).Map(mapFunc);
        }

        public static async Task<IEnumerable<TReturn>> MapAsync<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7, TValue8, TValue9, TValue10, TValue11, TValue12, TValue13, TValue14, TValue15, TReturn>(
            this IStatementResultCursor statementResultCursor,
            Func<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7, TValue8, TValue9, TValue10, TValue11, TValue12, TValue13, TValue14, TValue15, TReturn> mapFunc)
        {
            return (await statementResultCursor.ToListAsync()).Map(mapFunc);
        }

        public static async Task<IEnumerable<TReturn>> MapAsync<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7, TValue8, TValue9, TValue10, TValue11, TValue12, TValue13, TValue14, TValue15, TValue16, TReturn>(
            this IStatementResultCursor statementResultCursor,
            Func<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7, TValue8, TValue9, TValue10, TValue11, TValue12, TValue13, TValue14, TValue15, TValue16, TReturn> mapFunc)
        {
            return (await statementResultCursor.ToListAsync()).Map(mapFunc);
        }
    }
}
