using System;
using System.Collections.Generic;
using NLog;
using TagCloud.configurations;
using TagCloud.file_readers;
using TagCloud.filters;
using TagCloud.handlers;

namespace TagCloud
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            InitLogger();
            var reader = new TxtReader("simple_input.txt");
            var filters = new List<IWordFilter> { new LowerCaseWordFilter() };
            var configuration = new DefaultWordFilterConfiguration(filters);
            var wordRepository = new WordRepository(reader, configuration);
            foreach (var word in wordRepository.words)
                Console.WriteLine(word);
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