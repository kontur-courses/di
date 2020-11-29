using System;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using TagCloud.Layout;

namespace TagCloud
{
    class Program
    {
        private static IServiceProvider serviceProvider;
        private static CommandLineApplication app = new CommandLineApplication();
        static int Main(string[] args)
        {
            ConfigureServices();
            ConfigureCLI();
            return app.Execute(args);
        }
        
        private static void ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IPathCreater, PathCreater>();
            services.AddSingleton<IWordParser, OneWordInLineParser>();
            services.AddSingleton<IFrequencyAnalyzer, FrequencyAnalyzer>();
            services.AddSingleton<ICanvas>(_ => new Canvas(1000, 800));
            services.AddSingleton<ISpiral, Spiral>();
            services.AddSingleton<ILayouter, Layouter>();
            services.AddSingleton<IImageInfo, ImageInfo>();
            services.AddSingleton<IVisualizer, Visualizer>();

            serviceProvider = services.BuildServiceProvider();
        }

        private static void ConfigureCLI()
        {
            app.HelpOption();
            var optionInput = app.Option("-i|--input <INPUT>", "input filename", CommandOptionType.SingleValue);
            var optionFont = app.Option("-f|--font <FONT>", "font family", CommandOptionType.SingleValue);

            app.OnExecute(() =>
            {
                var visualizer = serviceProvider.GetService<IVisualizer>();
                var filename = optionInput.HasValue() ? optionInput.Value() : "input.txt";
                var fontFamily = optionFont.HasValue() ? optionFont.Value() : "Arial";
                visualizer.Visualize(filename, fontFamily);
                return 0;
            });
        }
    }
}