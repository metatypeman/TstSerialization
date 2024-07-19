using NLog;
using TestSandbox.SerializedObjects;

namespace TestSandbox
{
    internal class Program
    {
        private static ILogger _logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Case1();
        }

        private static void Case1()
        {
            _logger.Info("Begin");

            var engine = new Engine();

            _logger.Info($"engine = {engine}");

            _logger.Info("End");
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            _logger.Info($"e.ExceptionObject = {e.ExceptionObject}");
        }
    }
}
