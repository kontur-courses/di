using System.Collections.Generic;
using NLog;
using TagCloud.file_readers;
using Microsoft.Extensions.DependencyInjection;
using TagCloud.configurations;
using TagCloud.repositories;
using TagCloud.selectors;

namespace TagCloud
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            InitLogger();
            var services = new ServiceCollection()
                .AddSingleton<IFileReader>(new TxtReader("simple_input.txt"))
                .AddSingleton(new List<IWordHandler> {new ToLowerCaseHandler()})
                .AddSingleton(new List<IWordFilter>())
                .AddScoped<IWordRepositoryConfiguration, WordRepositoryConfiguration>()
                .AddScoped<WordRepository>();
            var container = services.BuildServiceProvider();
            var scope = container.CreateScope();
            var wordRepository = scope.ServiceProvider.GetRequiredService<WordRepository>();
            var stat = wordRepository.CalculateWordStatistics();
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