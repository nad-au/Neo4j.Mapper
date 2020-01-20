using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using ServiceStack;
using ServiceStack.Text;

namespace Neo4jMapper
{
    public static class PlatformExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAttributeNamed(this PropertyInfo pi, string name)
        {
            var normalizedAttr = name.Replace("Attribute", "").ToLower();
            return pi.AllAttributes().Any(x => x.GetType().Name.Replace("Attribute", "").ToLower() == normalizedAttr);
        }

        //Should only register Runtime Attributes on StartUp, So using non-ThreadSafe Dictionary is OK
        static Dictionary<string, List<Attribute>> propertyAttributesMap
            = new Dictionary<string, List<Attribute>>();

        internal static string UniqueKey(this PropertyInfo pi)
        {
            if (pi.DeclaringType == null)
                throw new ArgumentException("Property '{0}' has no DeclaringType".Fmt(pi.Name));

            return pi.DeclaringType.Namespace + "." + pi.DeclaringType.Name + "." + pi.Name;
        }

        public static List<Attribute> GetAttributes(this PropertyInfo propertyInfo)
        {
            return !propertyAttributesMap.TryGetValue(propertyInfo.UniqueKey(), out var propertyAttrs)
                ? new List<Attribute>()
                : propertyAttrs.ToList();
        }

        public static object[] AllAttributes(this PropertyInfo propertyInfo)
        {
            var attrs = propertyInfo.GetCustomAttributes(true);
            var runtimeAttrs = propertyInfo.GetAttributes();
            if (runtimeAttrs.Count == 0)
                return attrs;

            runtimeAttrs.AddRange(attrs.Cast<Attribute>());
            return runtimeAttrs.Cast<object>().ToArray();
        }

        private static readonly ConcurrentDictionary<Type, ObjectDictionaryDefinition> toObjectMapCache =
            new ConcurrentDictionary<Type, ObjectDictionaryDefinition>();

        internal class ObjectDictionaryDefinition
        {
            public Type Type;
            public readonly List<ObjectDictionaryFieldDefinition> Fields = new List<ObjectDictionaryFieldDefinition>();
            public readonly Dictionary<string, ObjectDictionaryFieldDefinition> FieldsMap = new Dictionary<string, ObjectDictionaryFieldDefinition>();

            public void Add(string name, ObjectDictionaryFieldDefinition fieldDef)
            {
                Fields.Add(fieldDef);
                FieldsMap[name] = fieldDef;
            }
        }

        public class ObjectDictionaryFieldDefinition
        {
            public string Name;
            public Type Type;

            public GetMemberDelegate GetValueFn;
            public SetMemberDelegate SetValueFn;

            public Type ConvertType;
            public GetMemberDelegate ConvertValueFn;
        }

        public static Dictionary<string, object> ToObjectDictionary(this object obj)
        {
            if (obj == null)
                return null;

            if (obj is Dictionary<string, object> alreadyDict)
                return alreadyDict;

            if (obj is IDictionary<string, object> interfaceDict)
                return new Dictionary<string, object>(interfaceDict);

            var to = new Dictionary<string, object>();
            if (obj is Dictionary<string, string> stringDict)
            {
                foreach (var entry in stringDict)
                {
                    to[entry.Key] = entry.Value;
                }
                return to;
            }

            if (obj is IDictionary d)
            {
                foreach (var key in d.Keys)
                {
                    to[key.ToString()] = d[key];
                }
                return to;
            }

            if (obj is NameValueCollection nvc)
            {
                for (var i = 0; i < nvc.Count; i++)
                {
                    to[nvc.GetKey(i)] = nvc.Get(i);
                }
                return to;
            }

            if (obj is IEnumerable<KeyValuePair<string, object>> objKvps)
            {
                foreach (var kvp in objKvps)
                {
                    to[kvp.Key] = kvp.Value;
                }
                return to;
            }
            if (obj is IEnumerable<KeyValuePair<string, string>> strKvps)
            {
                foreach (var kvp in strKvps)
                {
                    to[kvp.Key] = kvp.Value;
                }
                return to;
            }

            var type = obj.GetType();
            if (type.GetKeyValuePairsTypes(out var keyType, out var valueType, out var kvpType) && obj is IEnumerable e)
            {
                var keyGetter = TypeProperties.Get(kvpType).GetPublicGetter("Key");
                var valueGetter = TypeProperties.Get(kvpType).GetPublicGetter("Value");
                
                foreach (var entry in e)
                {
                    var key = keyGetter(entry);
                    var value = valueGetter(entry);
                    to[key.ConvertTo<string>()] = value;
                }
                return to;
            }
            

            if (obj is KeyValuePair<string, object> objKvp)
                return new Dictionary<string, object> { { nameof(objKvp.Key), objKvp.Key }, { nameof(objKvp.Value), objKvp.Value } };
            if (obj is KeyValuePair<string, string> strKvp)
                return new Dictionary<string, object> { { nameof(strKvp.Key), strKvp.Key }, { nameof(strKvp.Value), strKvp.Value } };
            
            if (type.GetKeyValuePairTypes(out _, out var _))
            {
                return new Dictionary<string, object> {
                    { "Key", TypeProperties.Get(type).GetPublicGetter("Key")(obj).ConvertTo<string>() },
                    { "Value", TypeProperties.Get(type).GetPublicGetter("Value")(obj) },
                };
            }

            if (!toObjectMapCache.TryGetValue(type, out var def))
                toObjectMapCache[type] = def = CreateObjectDictionaryDefinition(type);

            foreach (var fieldDef in def.Fields)
            {
                var value = fieldDef.GetValueFn(obj);

                to[fieldDef.Name] = ValueConvert((fieldDef, value));
            }

            return to;
        }

        public static Func<(ObjectDictionaryFieldDefinition fieldDefinition, object value), object> ValueConvert = o => o.fieldDefinition.Type.IsEnum ? o.value.ToString() : o.value;

        public static bool GetKeyValuePairsTypes(this Type dictType, out Type keyType, out Type valueType, out Type kvpType)
        {
            //matches IDictionary<,>, IReadOnlyDictionary<,>, List<KeyValuePair<string, object>>
            var genericDef = dictType.GetTypeWithGenericTypeDefinitionOf(typeof(IEnumerable<>));
            if (genericDef != null)
            {
                kvpType = genericDef.GetGenericArguments()[0];
                if (GetKeyValuePairTypes(kvpType, out keyType, out valueType)) 
                    return true;
            }
            kvpType = keyType = valueType = null;
            return false;
        }

        public static bool GetKeyValuePairTypes(this Type kvpType, out Type keyType, out Type valueType)
        {
            var genericKvps = kvpType.GetTypeWithGenericTypeDefinitionOf(typeof(KeyValuePair<,>));
            if (genericKvps != null)
            {
                var genericArgs = kvpType.GetGenericArguments();
                keyType = genericArgs[0];
                valueType = genericArgs[1];
                return true;
            }

            keyType = valueType = null;
            return false;
        }

        private static ObjectDictionaryDefinition CreateObjectDictionaryDefinition(Type type)
        {
            var def = new ObjectDictionaryDefinition
            {
                Type = type,
            };

            foreach (var pi in type.GetSerializableProperties())
            {
                def.Add(pi.Name, new ObjectDictionaryFieldDefinition
                {
                    Name = pi.Name,
                    Type = pi.PropertyType,
                    GetValueFn = pi.CreateGetter(),
                    SetValueFn = pi.CreateSetter(),
                });
            }

            if (JsConfig.IncludePublicFields)
            {
                foreach (var fi in type.GetSerializableFields())
                {
                    def.Add(fi.Name, new ObjectDictionaryFieldDefinition
                    {
                        Name = fi.Name,
                        Type = fi.FieldType,
                        GetValueFn = fi.CreateGetter(),
                        SetValueFn = fi.CreateSetter(),
                    });
                }
            }
            return def;
        }
    }
}
