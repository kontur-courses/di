using System;
using System.Diagnostics;
using System.Drawing;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using TagCloud.Coloring;
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
        
        private static void ConfigureServices(string size, string coloring)
        {
            var services = new ServiceCollection();

            services.AddSingleton<IPathCreater, PathCreater>();
            services.AddSingleton<IWordParser, LiteratureTextParser>();
            services.AddSingleton<IFrequencyAnalyzer, FrequencyAnalyzer.FrequencyAnalyzer>();
            ConfigureCanvas(services, size);
            services.AddSingleton<ISpiral, Spiral>();
            services.AddSingleton<ILayouter, Layouter>();
            services.AddSingleton<IImageInfo, ImageInfo>();
            ConfigureColoringService(services, coloring);
            services.AddSingleton<IVisualizer, Visualizer>();

            serviceProvider = services.BuildServiceProvider();
        }

        private static void ConfigureCanvas(ServiceCollection services, string size)
        {
            var arr = size.Split(',');
            if (arr.Length == 2 && int.TryParse(arr[0], out var width) && int.TryParse(arr[1], out var height))
            {
                services.AddSingleton<ICanvas>(_ => new Canvas(width, height));
            }
            else
            {
                throw new ArgumentException("bad size argument");
            }
        }

        private static void ConfigureColoringService(ServiceCollection services, string coloring)
        {
            switch(coloring)
            {
              case "location":
                  services.AddSingleton<IPainter, PainterColoringByLocation>();
                  break;
              case "random":
                  services.AddSingleton<IPainter, PainterRandomColoring>();
                  break;
              case "words":
                  services.AddSingleton<IPainter>(_ => new PainterWithoutRectangles(Color.Crimson));
                  break;
              default:
                  throw new ArgumentException("bad coloring");
            }
            
        }

        private static void ConfigureCLI()
        {
            app.HelpOption();
            var optionInput = app.Option("-i|--input <INPUT>", "input filename", CommandOptionType.SingleValue);
            var optionFont = app.Option("-f|--font <FONT>", "font family", CommandOptionType.SingleValue);
            var optionSize = app.Option("-s|--size <SIZE>", "size of image width,height", CommandOptionType.SingleValue);
            var optionColoring =
                app.Option("-c|--coloring <COLORS>", "coloring algoritm", CommandOptionType.SingleValue);

            
            app.OnExecute(() =>
            {
                var size = optionSize.HasValue() ? optionSize.Value() : "1000,800";
                var coloring = optionColoring.HasValue() ? optionColoring.Value() : "location";
                
                ConfigureServices(size, coloring);

                var visualizer = serviceProvider.GetService<IVisualizer>();
                var filename = optionInput.HasValue() ? optionInput.Value() : "cats2.txt";
                var fontFamily = optionFont.HasValue() ? optionFont.Value() : "Arial";
                visualizer.Visualize(filename, fontFamily);

                return 0;
            });
        }
    }
}