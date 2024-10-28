using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Threading;
using TestSandbox.Serialization;
using TestSandbox.SerializedObjects;

namespace TestSandbox
{
    internal class Program
    {
        private static ILogger _logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            TstConvertSerialiableProperties();
            //TstGetSerilizedProperties();
            //TstBase64();
            //TstLinkedCancellationToken();
            //TstCancellationToken();
            //TstCancellationTokenSource();
            //Case3_1();
            //Case3();
            //Case2_1();
            //Case2();
            //Case1();
            //ProcessDictionary();
            //ProcessQueue();
            //CreateGenericType();
        }

        private static void TstConvertSerialiableProperties()
        {
            var cls3 = new Class3();

            //var newcls3 = Convert.ChangeType(cls3, typeof(IClass3));

            var cls2 = new Class2();

            var cls2Type = cls2.GetType();

            var fields = cls2Type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            _logger.Info($"fields.Length = {fields.Length}");

            foreach (var field in fields)
            {
                _logger.Info($"field.Name = {field.Name}");
                _logger.Info($"field.FieldType.FullName = {field.FieldType.FullName}");

                field.SetValue(cls2, cls3);
            }
        }

        private static void TstGetSerilizedProperties()
        {
            var obj = new Class1();

#if DEBUG
            _logger.Info($"obj = {obj}");
#endif

            var objType = obj.GetType();

            //var fields = objType.GetFields(/* BindingFlags.Instance | BindingFlags.GetField | BindingFlags.DeclaredOnly*/);
            var fields = objType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

#if DEBUG
            _logger.Info($"fields.Length = {fields.Length}");
#endif

            foreach (var field in fields)
            {
#if DEBUG
                _logger.Info($"field.Name = {field.Name}");
                _logger.Info($"field.IsSpecialName = {field.IsSpecialName}");
                _logger.Info($"field.CustomAttributes.Count() = {field.CustomAttributes.Count()}");
#endif

                foreach(var attr in field.CustomAttributes)
                {
#if DEBUG
                    _logger.Info($"attr.AttributeType?.FullName = {attr.AttributeType?.FullName}");
#endif

                    foreach(var ctorArg in attr.ConstructorArguments)
                    {
#if DEBUG
                        _logger.Info($"ctorArg.Value = {ctorArg.Value}");
#endif
                    }

                    foreach (var namedArg in attr.NamedArguments)
                    {
#if DEBUG
                        _logger.Info($"namedArg.MemberName = {namedArg.MemberName}");
                        _logger.Info($"namedArg.TypedValue.Value = {namedArg.TypedValue.Value}");
#endif
                    }
                }

                field.SetValue(obj, 5);
            }

            var properties = objType.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

#if DEBUG
            _logger.Info($"properties.Length = {properties.Length}");
#endif

            foreach (var property in properties)
            {
#if DEBUG
                _logger.Info($"property.Name = {property.Name}");
                _logger.Info($"property.GetMethod == null = {property.GetMethod == null}");
                _logger.Info($"property.SetMethod == null = {property.SetMethod == null}");
#endif
            }

            var baseFields = objType.BaseType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            foreach (var baseField in baseFields)
            {
#if DEBUG
                _logger.Info($"baseField.Name = {baseField.Name}");
#endif

                baseField.SetValue(obj, 5);
            }

#if DEBUG
            _logger.Info($"obj (after) = {obj}");
#endif
        }

        private static void TstBase64()
        {
            var objPtr1 = new ObjectPtr("Hi! \"tmp\" TTT!", "Int");

#if DEBUG
            _logger.Info($"objPtr1 = {objPtr1}");
#endif

            var json1 = JsonConvert.SerializeObject(objPtr1, Formatting.None);

#if DEBUG
            _logger.Info($"json1 = '{json1}'");
#endif

            var base64_1 = ToBase64String(json1);

#if DEBUG
            _logger.Info($"base64_1 = '{base64_1}'");
#endif

            var jval1 = FromBase64String(base64_1);

#if DEBUG
            _logger.Info($"jval1 = '{jval1}'");
#endif

            var objPtr2 = new ObjectPtr("Hello!", "string");

#if DEBUG
            _logger.Info($"objPtr2 = {objPtr2}");
#endif

            var json2 = JsonConvert.SerializeObject(objPtr2, Formatting.None);

#if DEBUG
            _logger.Info($"json2 = '{json2}'");
#endif

            var base64_2 = ToBase64String(json2);

#if DEBUG
            _logger.Info($"base64_2 = '{base64_2}'");
#endif

#if DEBUG
            _logger.Info($"base64_1 == base64_2 = {base64_1 == base64_2}");
#endif

            var jval2 = FromBase64String(base64_2);

#if DEBUG
            _logger.Info($"jval2 = '{jval2}'");
#endif
        }

        public static string ToBase64String(string input)
        {
            var base64Array = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(base64Array);
        }

        public static string FromBase64String(string input)
        {
            var base64Array = Convert.FromBase64String(input);
            return Encoding.UTF8.GetString(base64Array);
        }

        private static void TstLinkedCancellationToken()
        {
            var cancellationTokenSource1 = new CancellationTokenSource();
            var cancellationTokenSource2 = new CancellationTokenSource();

            var linkedCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationTokenSource1.Token, cancellationTokenSource2.Token);
        }

        private static void TstCancellationToken()
        {
            var cancellationTokenSource = new CancellationTokenSource();

#if DEBUG
            _logger.Info($"cancellationTokenSource.GetType().FullName = '{cancellationTokenSource.GetType().FullName}'");
            _logger.Info($"cancellationTokenSource.GetType().Name = '{cancellationTokenSource.GetType().Name}'");
#endif

            _logger.Info($"cancellationTokenSource = {JsonConvert.SerializeObject(cancellationTokenSource, Formatting.Indented)}");
            _logger.Info($"cancellationTokenSource.GetHashCode() = {cancellationTokenSource.GetHashCode()}");

            var token = cancellationTokenSource.Token;

#if DEBUG
            _logger.Info($"token.GetType().FullName = '{token.GetType().FullName}'");
            _logger.Info($"token.GetType().Name = '{token.GetType().Name}'");
#endif

            var fields = token.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField | BindingFlags.DeclaredOnly);

#if DEBUG
            _logger.Info($"fields.Length = {fields.Length}");
#endif

            foreach (var field in fields)
            {
#if DEBUG
                _logger.Info($"field.Name = {field.Name}");
#endif

                if (field.Name == "_source")
                {
                    var fieldValue = field.GetValue(token);

                    _logger.Info($"fieldValue = {JsonConvert.SerializeObject(fieldValue, Formatting.Indented)}");
                    _logger.Info($"fieldValue.GetHashCode() = {fieldValue.GetHashCode()}");
                }
            }

            var tokenContent = JsonConvert.SerializeObject(token, Formatting.Indented);

#if DEBUG
            _logger.Info($"tokenContent = {tokenContent}");
#endif

            var token2 = JsonConvert.DeserializeObject<CancellationToken>(tokenContent);

#if DEBUG
            _logger.Info($"token2 = {JsonConvert.SerializeObject(token2, Formatting.Indented)}");
#endif
        }

        private static void TstCancellationTokenSource()
        {
            var cancellationTokenSource = new CancellationTokenSource();

            var cancellationTokenSourceContent = JsonConvert.SerializeObject(cancellationTokenSource, Formatting.Indented);

#if DEBUG
            _logger.Info($"cancellationTokenSourceContent = {cancellationTokenSourceContent}");
#endif

            var cancellationTokenSource2 = JsonConvert.DeserializeObject<CancellationTokenSource>(cancellationTokenSourceContent);

#if DEBUG
            _logger.Info($"cancellationTokenSource2 = {JsonConvert.SerializeObject(cancellationTokenSource2, Formatting.Indented)}");
#endif
        }

        private static void Case3_1()
        {
            _logger.Info("Begin");

            var deserializationContext = new DeserializationContext(@"D:\Repos\TstSerialization\TestSandbox\bin\Debug\net8.0\ba6c933a-dbe3-4093-8017-919d9dca4be1");

            var deserializer = new Deserializer(deserializationContext);

            var objWithCollectionsInProps = deserializer.Deserialize<ObjWithCollectionsInProps>();

            _logger.Info($"objWithCollectionsInProps = {objWithCollectionsInProps}");

            _logger.Info("End");
        }

        private static void Case3()
        {
            _logger.Info("Begin");

            var deserializationContext = new DeserializationContext(@"d:\Repos\TstSerialization\TestSandbox\bin\Debug\net8.0\2c33fe67-1edf-48ab-ad78-17b3ed577be3\");

            var deserializer = new Deserializer(deserializationContext);

            var engine = deserializer.Deserialize<Engine>();

            _logger.Info($"engine = {engine}");

            _logger.Info("End");
        }

        private static void Case2_1()
        {
            _logger.Info("Begin");

            var objWithCollectionsInProps = new ObjWithCollectionsInProps();
            objWithCollectionsInProps.ObjListProp =
            [
                1,
                new SecondComponentData() { SomeField = 15},
                null
            ];

            _logger.Info($"objWithCollectionsInProps = {objWithCollectionsInProps}");

            var serializationContext = new SerializationContext();

            var serializer = new Serializer(serializationContext);

            serializer.Serialize(objWithCollectionsInProps);

            _logger.Info("End");
        }

        private static void Case2()
        {
            _logger.Info("Begin");

            var engine = new Engine();
            engine._engineContext.FirstComponent._data.Field1 = 16;
            engine._engineContext.SecondComponent._data.SomeField = 42;

            _logger.Info($"engine = {engine}");

            var serializationContext = new SerializationContext();

            var serializer = new Serializer(serializationContext);

            serializer.Serialize(engine);

            _logger.Info("End");
        }

        private static void Case1()
        {
            _logger.Info("Begin");

            var engine = new Engine();
            engine._engineContext.FirstComponent._data.Field1 = 16;
            engine._engineContext.SecondComponent._data.SomeField = 42;

            _logger.Info($"engine = {engine}");

            _logger.Info("End");
        }

        private static void ProcessDictionary()
        {
            _logger.Info("Begin");

            var dict = new Dictionary<int, string>();
            dict[1] = "Hi!";
            dict[5] = "Hello!";

            IDictionary iDict = dict;

            foreach(var item in iDict)
            {
                _logger.Info($"item.GetType().FullName = {item.GetType().FullName}");

                var dictEntry = (DictionaryEntry)item;

                _logger.Info($".GetType().FullName = {dictEntry.Key.GetType().FullName}");
                _logger.Info($".GetType().FullName = {dictEntry.Value.GetType().FullName}");
            }

            _logger.Info("End");
        }

        private static void ProcessQueue()
        {
            _logger.Info("Begin");

            var queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(3);
            queue.Enqueue(5);
            queue.Enqueue(16);

#if DEBUG
            _logger.Info($"queue = {JsonConvert.SerializeObject(queue, Formatting.Indented)}");
#endif

            var enumerable = (IEnumerable)queue;

#if DEBUG
            var type = enumerable.GetType();
            _logger.Info($"type.FullName = {type.FullName}");
            _logger.Info($"type.Name = {type.Name}");
            _logger.Info($"type.IsGenericType = {type.IsGenericType}");
#endif

            var genericParameterType = type.GetGenericArguments()[0];

#if DEBUG
            _logger.Info($"genericParameterType.FullName = {genericParameterType.FullName}");
            _logger.Info($"genericParameterType.Name = {genericParameterType.Name}");
            _logger.Info($"genericParameterType.IsGenericType = {genericParameterType.IsGenericType}");
            _logger.Info($"genericParameterType.IsPrimitive = {genericParameterType.IsPrimitive}");
#endif

            var genericListType = typeof(List<>).MakeGenericType(genericParameterType);

            _logger.Info($"genericListType.FullName = {genericListType.FullName}");

            var listWithPlainObjects = (IList)Activator.CreateInstance(genericListType);

            _logger.Info($"listWithPlainObjects.GetType() = {listWithPlainObjects.GetType().FullName}");


            foreach (var item in enumerable)
            {
#if DEBUG
                _logger.Info($"item = {JsonConvert.SerializeObject(item, Formatting.Indented)}");
#endif

                listWithPlainObjects.Add(item);
            }

#if DEBUG
            _logger.Info($"listWithPlainObjects = {JsonConvert.SerializeObject(listWithPlainObjects, Formatting.Indented)}");
#endif

            var queue2Obj = Activator.CreateInstance(type, listWithPlainObjects);


#if DEBUG
            _logger.Info($"queue2Obj = {JsonConvert.SerializeObject(queue2Obj, Formatting.Indented)}");
#endif

            var queue2 = (Queue<int>)queue2Obj;

#if DEBUG
            _logger.Info($"queue2 = {JsonConvert.SerializeObject(queue2, Formatting.Indented)}");
#endif

            _logger.Info("End");
        }

        private static void CreateGenericType()
        {
            _logger.Info("Begin");

            var type = typeof(int); // var type = itemType : put this line to fit the method
            var genericListType = typeof(List<>).MakeGenericType(type);

            _logger.Info($"genericListType.FullName = {genericListType.FullName}");

            var genericList = Activator.CreateInstance(genericListType);

            _logger.Info($"genericList.GetType() = {genericList.GetType().FullName}");

            _logger.Info("End");
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            _logger.Info($"e.ExceptionObject = {e.ExceptionObject}");
        }
    }
}
