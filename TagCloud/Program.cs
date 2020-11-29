using System;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using TagCloud.FrequencyAnalyzer;
using TagCloud.Layout;

namespace TagCloud
{
    class Program
    {
        private static IServiceProvider serviceProvider;
        private static CommandLineApplication app = new CommandLineApplication();
        static int Main(string[] args)
        {
            ConfigureCLI();
            return app.Execute(args);
        }
        
        private static void ConfigureServices(int canvasWidth, int canvasHeight)
        {
            var services = new ServiceCollection();

            services.AddSingleton<IPathCreater, PathCreater>();
            services.AddSingleton<IWordParser, OneWordInLineParser>();
            services.AddSingleton<IFrequencyAnalyzer, FrequencyAnalyzer.FrequencyAnalyzer>();
            services.AddSingleton<ICanvas>(_ => new Canvas(canvasWidth, canvasHeight));
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
            var optionSize = app.Option("-s|--size <SIZE>", "size of image width,height", CommandOptionType.SingleValue);

            app.OnExecute(() =>
            {
                if (optionSize.HasValue())
                {
                    ConfigureServices(optionSize.Value());
                }
                else
                {
                    ConfigureServices(1000, 800);
                }

                var visualizer = serviceProvider.GetService<IVisualizer>();
                var filename = optionInput.HasValue() ? optionInput.Value() : "input.txt";
                var fontFamily = optionFont.HasValue() ? optionFont.Value() : "Arial";
                visualizer.Visualize(filename, fontFamily);

                return 0;
            });
        }

        private static void ConfigureServices(string argumentValue)
        {
            var arr = argumentValue.Split(',');
            if (arr.Length == 2 && int.TryParse(arr[0], out var width) && int.TryParse(arr[1], out var height))
            {
                ConfigureServices(width, height);
            }
            else
            {
                throw new ArgumentException("bad size argument");
            }
        }
    }
}