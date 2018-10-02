using Neo4j.Driver.V1;
using ServiceStack;

namespace ConsoleApp1
{
    public static class RecordExtensions
    {
        public static T MapFromNode<T>(this IRecord record, string key)
        {
            return record[key].As<INode>().Properties.FromObjectDictionary<T>();
        }
    }
}
