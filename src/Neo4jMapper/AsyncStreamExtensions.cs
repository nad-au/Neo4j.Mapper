using System;
using System.Collections.Generic;
using System.Linq;
using Neo4j.Driver.V1;

namespace Neo4jMapper
{
    public static class AsyncStreamExtensions
    {
        public static IAsyncEnumerable<TReturn> Map<TReturn>(
            this IAsyncEnumerable<IRecord> records)
        {
            return records.Select(record => record.Map<TReturn>());
        }
    }
}
