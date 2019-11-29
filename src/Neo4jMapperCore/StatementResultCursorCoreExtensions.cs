using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Neo4j.Driver.V1;

namespace Neo4jMapper
{
    public static class StatementResultCursorCoreExtensions
    {
        #region Overloads

        public static IAsyncEnumerable<TReturn> AsyncMap<TReturn>(
   this IStatementResultCursor result)
        {
            return result.AsyncMap(
                record => record.Map<TReturn>());
        }

        public static IAsyncEnumerable<TReturn> AsyncMap<TValue1, TValue2, TReturn>(
            this IStatementResultCursor result,
            Func<TValue1, TValue2, TReturn> mapFunc)
        {
            return result.AsyncMap(
                record => record.Map(mapFunc));
        }

        public static IAsyncEnumerable<TReturn> AsyncMap<TValue1, TValue2, TValue3, TReturn>(
            this IStatementResultCursor result,
            Func<TValue1, TValue2, TValue3, TReturn> mapFunc)
        {
            return result.AsyncMap(
                record => record.Map(mapFunc));
        }

        public static IAsyncEnumerable<TReturn> AsyncMap<TValue1, TValue2, TValue3, TValue4, TReturn>(
            this IStatementResultCursor result,
            Func<TValue1, TValue2, TValue3, TValue4, TReturn> mapFunc)
        {
            return result.AsyncMap(
                record => record.Map(mapFunc));
        }

        public static IAsyncEnumerable<TReturn> AsyncMap<TValue1, TValue2, TValue3, TValue4, TValue5, TReturn>(
            this IStatementResultCursor result,
            Func<TValue1, TValue2, TValue3, TValue4, TValue5, TReturn> mapFunc)
        {
            return result.AsyncMap(
                record => record.Map(mapFunc));
        }

        public static IAsyncEnumerable<TReturn> AsyncMap<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TReturn>(
            this IStatementResultCursor result,
            Func<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TReturn> mapFunc)
        {
            return result.AsyncMap(
                record => record.Map(mapFunc));
        }

        public static IAsyncEnumerable<TReturn> AsyncMap<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7, TReturn>(
            this IStatementResultCursor result,
            Func<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7, TReturn> mapFunc)
        {
            return result.AsyncMap(
                record => record.Map(mapFunc));
        }

        public static IAsyncEnumerable<TReturn> AsyncMap<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7, TValue8, TReturn>(
            this IStatementResultCursor result,
            Func<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7, TValue8, TReturn> mapFunc)
        {
            return result.AsyncMap(
                record => record.Map(mapFunc));
        }

        public static IAsyncEnumerable<TReturn> AsyncMap<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7, TValue8, TValue9, TReturn>(
            this IStatementResultCursor result,
            Func<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7, TValue8, TValue9, TReturn> mapFunc)
        {
            return result.AsyncMap(
                record => record.Map(mapFunc));
        }

        public static IAsyncEnumerable<TReturn> AsyncMap<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7, TValue8, TValue9, TValue10, TReturn>(
            this IStatementResultCursor result,
            Func<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7, TValue8, TValue9, TValue10, TReturn> mapFunc)
        {
            return result.AsyncMap(
                record => record.Map(mapFunc));
        }

        public static IAsyncEnumerable<TReturn> AsyncMap<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7, TValue8, TValue9, TValue10, TValue11, TReturn>(
            this IStatementResultCursor result,
            Func<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7, TValue8, TValue9, TValue10, TValue11, TReturn> mapFunc)
        {
            return result.AsyncMap(
                record => record.Map(mapFunc));
        }

        public static IAsyncEnumerable<TReturn> AsyncMap<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7, TValue8, TValue9, TValue10, TValue11, TValue12, TReturn>(
            this IStatementResultCursor result,
            Func<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7, TValue8, TValue9, TValue10, TValue11, TValue12, TReturn> mapFunc)
        {
            return result.AsyncMap(
                record => record.Map(mapFunc));
        }

        public static IAsyncEnumerable<TReturn> AsyncMap<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7, TValue8, TValue9, TValue10, TValue11, TValue12, TValue13, TReturn>(
            this IStatementResultCursor result,
            Func<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7, TValue8, TValue9, TValue10, TValue11, TValue12, TValue13, TReturn> mapFunc)
        {
            return result.AsyncMap(
                record => record.Map(mapFunc));
        }

        public static IAsyncEnumerable<TReturn> AsyncMap<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7, TValue8, TValue9, TValue10, TValue11, TValue12, TValue13, TValue14, TReturn>(
            this IStatementResultCursor result,
            Func<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7, TValue8, TValue9, TValue10, TValue11, TValue12, TValue13, TValue14, TReturn> mapFunc)
        {
            return result.AsyncMap(
                record => record.Map(mapFunc));
        }

        public static IAsyncEnumerable<TReturn> AsyncMap<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7, TValue8, TValue9, TValue10, TValue11, TValue12, TValue13, TValue14, TValue15, TReturn>(
            this IStatementResultCursor result,
            Func<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7, TValue8, TValue9, TValue10, TValue11, TValue12, TValue13, TValue14, TValue15, TReturn> mapFunc)
        {
            return result.AsyncMap(
                record => record.Map(mapFunc));
        }

        public static IAsyncEnumerable<TReturn> AsyncMap<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7, TValue8, TValue9, TValue10, TValue11, TValue12, TValue13, TValue14, TValue15, TValue16, TReturn>(
            this IStatementResultCursor result,
            Func<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7, TValue8, TValue9, TValue10, TValue11, TValue12, TValue13, TValue14, TValue15, TValue16, TReturn> mapFunc)
        {
            return result.AsyncMap(
                record => record.Map(mapFunc));
        } 

        #endregion // Overloads

        public static async IAsyncEnumerable<TReturn> AsyncMap<TReturn>(
            this IStatementResultCursor result,
            Func<IRecord, TReturn> mapFunc)
        {
            var list = new List<TReturn>();
            while (await result.FetchAsync().ConfigureAwait(false))
            {
                TReturn item = mapFunc(result.Current);
                yield return item;
            }
        }
    }
}
