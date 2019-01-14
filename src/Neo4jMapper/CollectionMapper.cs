using System;
using System.Collections;
using System.Collections.Generic;
using ServiceStack.Text;

namespace Neo4jMapper
{
    public interface ICollectionMapper
    {
        IEnumerable MapValues(IEnumerable fromList, Type toInstanceOfType);
    }

    public class CollectionMapper<T> : ICollectionMapper
    {
        public IEnumerable MapValues(IEnumerable fromList, Type toInstanceOfType)
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
