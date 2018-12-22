using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mime;
using Autofac;
using Newtonsoft.Json;
using WordCloudImageGenerator;
using WordCloudImageGenerator.LayoutCraetion.Layouters;
using WordCloudImageGenerator.Layouting.Layouters;
using WordCloudImageGenerator.Layouting.Layouters.Circular;
using WordCloudImageGenerator.Parsing.BlackList;
using WordCloudImageGenerator.Parsing.Extractors;

namespace WordCloudConsole
{
    class Program
    {
        private static IContainer Container { get; set; }
        private static WordCloudConfig _config;

        static void Main(string[] args)
        {
            ConfigureContainer();
            ResolveConfiguration(args);
            RunCloudRenderingCycle();
        }

        private static void RunCloudRenderingCycle()
        {
            TagCloudRenderer renderer;

            using (var scope = Container.BeginLifetimeScope())
                renderer = scope.Resolve<TagCloudRenderer>();

            while (true)
            {
                Console.WriteLine("Enter the path to the file containing the words or E to exit:");
                var pathOrExit = Console.ReadLine();
                if (pathOrExit == "E")
                    return;
                if (!File.Exists(pathOrExit))
                {
                    Console.WriteLine("Invalid path");
                    continue;
                }

                var words = File.ReadAllText(pathOrExit);
                var layoutImagePath = renderer.GetLayout(words);
                Console.WriteLine($"Image saved to: {layoutImagePath}");
            }
        }

        private static void ResolveConfiguration(string[] args)
        {
            string configPath;

            if (!args.Any())
            {
                SetDefaultTagCloudConfig();
                configPath = Path.Combine(Directory.GetCurrentDirectory(), "defaultTagCloudConfig.json");
                Console.WriteLine(
                    "Tag cloud options are left as default because Configuration was not specified at startup.");
            }
            else
                configPath = args[0];

            var configJson = File.ReadAllText(configPath);
            _config = JsonConvert.DeserializeObject<WordCloudConfig>(configJson);
            Console.WriteLine("Current configuration is:");
            Console.WriteLine($"Layout type:{_config.LayoutType.ToString()}");
            Console.WriteLine($"Min font size:{_config.MinFontSize}");
            Console.WriteLine($"Max font size:{_config.MaxFontSize}");
        }

        private static void SetDefaultTagCloudConfig()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var defaultConfig = JsonConvert.SerializeObject(scope.Resolve<WordCloudConfig>());
                var configPath = Path.Combine(Directory.GetCurrentDirectory(), "defaultTagCloudConfig.json");
                File.WriteAllText(configPath, defaultConfig);
            }
        }

        private static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<CommonWords>().As<IBlackList>();
            builder.RegisterType<SimpleExtractor>().As<IWordExtractor>();
            builder.RegisterType<Vizualizer>().AsSelf();

            builder.RegisterType<WordCloudConfig>().AsSelf();
            builder.RegisterType<TagCloudRenderer>().AsSelf();
            builder.Register(_ => LayoutTypes.Circular).As<LayoutTypes>();

            var palette = new List<Brush>()
            {
                Brushes.CadetBlue,
                Brushes.DeepPink,
                Brushes.Coral,
                Brushes.GreenYellow,
                Brushes.Green,
            };

            builder.RegisterInstance(palette).As<IEnumerable<Brush>>();
            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
            builder.RegisterType<Vizualizer>().As<ITagCloudVizualizer>();
            builder.Register(_ => new Point(0, 0)).As<Point>();
            Container = builder.Build();
        }
    }
}
