using System;
using System.Collections.Generic;
using Neo4j.Driver.V1;
using ServiceStack;

namespace ConsoleApp1
{
    public static class RecordExtensions
    {
        public static TReturn Map<TReturn>(
            this IRecord record)
        {
            return record[0].MapValue<TReturn>();
        }

        public static TReturn Map<T1, T2, TReturn>(
            this IRecord record,
            Func<T1, T2, TReturn> map)
        {
            return map(
                record[0].MapValue<T1>(), 
                record[1].MapValue<T2>());
        }

        public static TReturn Map<T1, T2, T3, TReturn>(
            this IRecord record,
            Func<T1, T2, T3, TReturn> map)
        {
            return map(
                record[0].MapValue<T1>(), 
                record[1].MapValue<T2>(),
                record[2].MapValue<T3>());
        }

        private static T MapValue<T>(this object value)
        {
            if (value is INode node)
            {
                return node.Properties.FromObjectDictionary<T>();
            }

            if (value is IReadOnlyDictionary<string, object> map)
            {
                return map.FromObjectDictionary<T>();
            }

            return value.As<T>();
        }
    }
}
