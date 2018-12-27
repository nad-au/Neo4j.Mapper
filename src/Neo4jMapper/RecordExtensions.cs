using System;
using Neo4j.Driver.V1;

namespace Neo4jMapper
{
    public static class RecordExtensions
    {
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
