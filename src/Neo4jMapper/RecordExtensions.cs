using System;
using System.Collections.Generic;
using System.Linq;
using Neo4j.Driver.V1;

namespace Neo4jMapper
{
    public static class RecordExtensions
    {
        public static IEnumerable<TReturn> Map<TReturn>(
            this IEnumerable<IRecord> records)
        {
            return records.Select(record => record.Map<TReturn>());
        }

        public static IEnumerable<TReturn> Map<T1, T2, TReturn>(
            this IEnumerable<IRecord> records,
            Func<T1, T2, TReturn> mapFunc)
        {
            return records.Select(record => record.Map(mapFunc));
        }

        public static IEnumerable<TReturn> Map<T1, T2, T3, TReturn>(
            this IEnumerable<IRecord> records,
            Func<T1, T2, T3, TReturn> mapFunc)
        {
            return records.Select(record => record.Map(mapFunc));
        }

        public static TReturn Map<TReturn>(
            this IRecord record)
        {
            return ValueMapper.MapValue<TReturn>(record[0]);
        }

        public static TReturn Map<T1, T2, TReturn>(
            this IRecord record,
            Func<T1, T2, TReturn> map)
        {
            return map(
                ValueMapper.MapValue<T1>(record[0]), 
                ValueMapper.MapValue<T2>(record[1]));
        }

        public static TReturn Map<T1, T2, T3, TReturn>(
            this IRecord record,
            Func<T1, T2, T3, TReturn> map)
        {
            return map(
                ValueMapper.MapValue<T1>(record[0]), 
                ValueMapper.MapValue<T2>(record[1]),
                ValueMapper.MapValue<T3>(record[2]));
        }
    }
}
