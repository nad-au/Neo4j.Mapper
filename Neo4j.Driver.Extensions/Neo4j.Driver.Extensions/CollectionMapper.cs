using System;
using System.Collections;
using System.Collections.Generic;
using ServiceStack.Text;

namespace Neo4j.Driver.Extensions
{
    public interface ICollectionMapper
    {
        object MapValues(IEnumerable fromList, Type toInstanceOfType);
    }

    public class CollectionMapper<T> : ICollectionMapper
    {
        public object MapValues(IEnumerable fromList, Type toInstanceOfType)
        {
            var to = (ICollection<T>)TranslateListWithElements<T>.CreateInstance(toInstanceOfType);
            foreach (var item in fromList)
            {
                to.Add(ValueMapper.MapValue<T>(item));
            }
            return to;
        }
    }
}
