using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ServiceStack;

namespace Neo4j.Driver.Extensions
{
    public interface ITypeConverter<T>
    {
        object TranslateToGenericICollection(
            IEnumerable fromList, Type toInstanceOfType);
    }

    public class TypeConverter<T> : ITypeConverter<T>
    {
        public static object CreateInstance(Type toInstanceOfType)
        {
            if (toInstanceOfType.IsGenericType)
            {
                if (toInstanceOfType.HasAnyTypeDefinitionsOf(
                    typeof(ICollection<>), typeof(IList<>)))
                {
                    return typeof(List<T>).CreateInstance();
                }
            }

            return toInstanceOfType.CreateInstance();
        }

        public object TranslateToGenericICollection(
            IEnumerable fromList, Type toInstanceOfType)
        {
            var to = (ICollection<T>)CreateInstance(toInstanceOfType);
            foreach (var item in fromList)
            {
                to.Add(item.MapValue<T>());
            }
            return to;
        }

    }
}
