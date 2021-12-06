using NLog;
using TagCloud.file_readers;

namespace TagCloud
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            InitLogger();
            var reader = new TxtReader("simple_input.txt");
        }

        private static void InitLogger()
        {
            var config = new NLog.Config.LoggingConfiguration();

            var logfile = new NLog.Targets.FileTarget("logfile") {FileName = "logs.txt"};
            var logconsole = new NLog.Targets.ConsoleTarget("logconsole");

            config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);

            LogManager.Configuration = config;
        }
    }
}