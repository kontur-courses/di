using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using NLog;
using TagCloud.file_readers;
using Microsoft.Extensions.DependencyInjection;
using TagCloud.configurations;
using TagCloud.layouter;
using TagCloud.repositories;
using TagCloud.selectors;
using TagCloud.visual;

namespace TagCloud
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            InitLogger();
            var services = new ServiceCollection()
                .AddSingleton<IFileReader>(new TxtReader("simple_input.txt"))
                .AddSingleton(new List<IWordHandler> { new ToLowerCaseHandler() })
                .AddSingleton(new List<IWordFilter> { new EmptyFilter() })
                .AddSingleton<IWordRepositoryConfiguration, WordRepositoryConfiguration>()
                .AddSingleton<IRepository<string>, WordRepository>()
                .AddSingleton<IRepository<Tag>, TagRepository>()
                .AddSingleton<ITagRepositoryConfiguration>(
                    new TagRepositoryRepositoryConfiguration(Color.Gold, FontFamily.GenericSerif, 10)
                )
                .AddSingleton<ICloudLayouter, CircularCloudLayouter>()
                .AddSingleton<IVisualizer, TagVisualizer>()
                .AddSingleton<IImageConfiguration>(new ImageConfiguration(Color.Black, 1500, 1500))
                .AddSingleton<IImageSaveConfiguration>(new ImageSaveConfiguration("serious_out.png", ImageFormat.Png))
                .AddSingleton<ISaver, TagVisualizationSaver>();
            var container = services.BuildServiceProvider();
            var scope = container.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<ISaver>();
            service.Save();
        }

        private static void InitLogger()
        {
            var config = new NLog.Config.LoggingConfiguration();

            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "logs.txt" };
            var logconsole = new NLog.Targets.ConsoleTarget("logconsole");

            config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);

            LogManager.Configuration = config;
        }
    }
}