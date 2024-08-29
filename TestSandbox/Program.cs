using Newtonsoft.Json;
using NLog;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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

            CancellationToken();
            //CancellationTokenSource();
            //Case3_1();
            //Case3();
            //Case2_1();
            //Case2();
            //Case1();
            //ProcessQueue();
            //CreateGenericType();
        }

        private static void CancellationToken()
        {
            var cancellationTokenSource = new CancellationTokenSource();

            _logger.Info($"cancellationTokenSource = {JsonConvert.SerializeObject(cancellationTokenSource, Formatting.Indented)}");
            _logger.Info($"cancellationTokenSource.GetHashCode() = {cancellationTokenSource.GetHashCode()}");

            var token = cancellationTokenSource.Token;

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

        private static void CancellationTokenSource()
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
