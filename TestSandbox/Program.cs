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

            Case3();
            //Case2();
            //Case1();
        }

        private static void Case3()
        {
            _logger.Info("Begin");

            var deserializer = new Deserializer();

            var engine = deserializer.Deserialize<Engine>();

            _logger.Info($"engine = {engine}");

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

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            _logger.Info($"e.ExceptionObject = {e.ExceptionObject}");
        }
    }
}
