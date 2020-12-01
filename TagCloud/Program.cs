using System;
using System.Drawing;
using FluentAssertions;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using TagCloud.BackgroundPainter;
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
            ConfigureBackgroundPainterService(services, coloring);
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

        private static void ConfigureBackgroundPainterService(ServiceCollection services, string backgroundType)
        {
            switch(backgroundType)
            {
              case "rectangles":
                  services.AddSingleton<IBackgroundPainter, BackgroundPainterRectangles>();
                  break;
              case "empty":
                  services.AddSingleton<IBackgroundPainter, BackgroundPainterEmpty>();
                  break;
              default:
                  throw new ArgumentException("bad background type");
            }
            
        }

        private static Color ParseColor(string colorInRGB)
        {
            var arr = colorInRGB.Split(',');
            if (arr.Length == 3
                && TryGetColorComponent(arr[0], out var red)
                && TryGetColorComponent(arr[1], out var green)
                && TryGetColorComponent(arr[1], out var blue))
            {
                return Color.FromArgb(red, green, blue);
            }
            throw new ArgumentException("bad color for string");
        }

        private static bool TryGetColorComponent(string colorComponent, out int value)
        {
            if (int.TryParse(colorComponent, out var intColorComponent))
            {
                value = intColorComponent;
                return intColorComponent >= 0 && intColorComponent <= 255;
            }

            value = 0;
            return false;
        }

        private static void ConfigureCLI()
        {
            app.HelpOption();
            var optionInput = app.Option("-i|--input <INPUT>", "input filename", CommandOptionType.SingleValue);
            var optionFont = app.Option("-f|--font <FONT>", "font family", CommandOptionType.SingleValue);
            var optionSize = app.Option("-s|--size <SIZE>", "size of image width,height", CommandOptionType.SingleValue);
            var optionBackground = app.Option("-b|--backgound <BACKGROUND_STYLE>", "background style rectangles|empty", CommandOptionType.SingleValue);
            var optionStringColor =
                app.Option("-c|--color <COLOR>", "string color r,g,b", CommandOptionType.SingleValue);


            app.OnExecute(() =>
            {
                var size = optionSize.HasValue() ? optionSize.Value() : "1000,800";
                var coloring = optionBackground.HasValue() ? optionBackground.Value() : "empty";
                
                ConfigureServices(size, coloring);

                var visualizer = serviceProvider.GetService<IVisualizer>();
                var filename = optionInput.HasValue() ? optionInput.Value() : "input.txt";
                var fontFamily = optionFont.HasValue() ? optionFont.Value() : "Arial";
                var color = optionStringColor.HasValue() ? ParseColor(optionStringColor.Value()) : Color.Black;
                visualizer.Visualize(filename, fontFamily, color);

                return 0;
            });
        }
    }
}