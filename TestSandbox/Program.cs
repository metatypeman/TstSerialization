using NLog;
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

            Case3_1();
            //Case3();
            //Case2_1();
            //Case2();
            //Case1();
            //CreateGenericType();
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
