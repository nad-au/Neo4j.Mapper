using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Neo4j.Driver.V1;

namespace ConsoleApp1
{
    public static class RecordExtensions
    {
        public static IEnumerable<T> Project<T>(this IStatementResult records, Expression<Func<IField, T>> expression)
        {
            var key = expression.Parameters.First().Name;
            var func = expression.Compile();

            return records.Select(r => func(new Field(r, key)));
        }

        public static IEnumerable<T> Project<T>(this IStatementResult records, Expression<Func<IField, IField, T>> expression)
        {
            var key = expression.Parameters.First().Name;
            throw new NotImplementedException();
            var func = expression.Compile();
            foreach (var record in records)
            {
            }
        }
    }
}
