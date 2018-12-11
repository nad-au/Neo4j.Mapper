using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Neo4j.Driver.V1;
using ServiceStack;

namespace ConsoleApp1
{
    public class ResultProcessor
    {
        private readonly IRecord _record;

        private CypherResultItem item;

        public ResultProcessor(IRecord record)
        {
            _record = record;
        }

        public void Define<T>(Expression<Func<ICypherResultItem, T>> expression)
        {
            var parameterExpression = expression.Parameters[0];
            var memberName = parameterExpression.Name;
            item = new CypherResultItem(_record, memberName);
        }

        public T Execute<T>()
        {
            return item.To<T>();
        }

        public T ExecuteAsNode<T>()
        {
            return item.ToNode<T>();
        }
    }


    public class CypherResultItem : ICypherResultItem
    {
        private readonly IRecord _record;
        private readonly string _key;

        public CypherResultItem(IRecord record, string key)
        {
            _record = record;
            _key = key;
        }
        
        public T To<T>()
        {
            return _record[_key].As<T>();
        }

        public T ToNode<T>()
        {
            return (_record[_key] as INode).Properties.FromObjectDictionary<T>();
        }


    }
    public interface ICypherResultItem
    {
        T To<T>();
    }
}
