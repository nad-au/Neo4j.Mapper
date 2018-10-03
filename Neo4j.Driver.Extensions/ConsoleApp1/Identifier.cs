using Neo4j.Driver.V1;
using ServiceStack;

namespace ConsoleApp1
{
    public class Field : IField
    {
        private readonly IRecord record;
        private readonly string key;

        public Field(IRecord record, string key)
        {
            this.record = record;
            this.key = key;
        }

        public T As<T>()
        {
            return record[key].As<T>();
        }

        public T AsNode<T>()
        {
            return record[key].As<INode>().Properties.FromObjectDictionary<T>();
        }
    }
}
