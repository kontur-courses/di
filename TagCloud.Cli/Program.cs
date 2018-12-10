using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CommandLine;
using TagCloud;
using TagCloud.Enums;
using TagCloud.Interfaces;
using TagCloud.Layouter;
using TagCloud.Visualizer;
using Point = TagCloud.Layouter.Point;
using Size = System.Drawing.Size;

namespace TagCloudCreator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var configuration = ParseArguments(args);
            var container = MakeContainer(configuration);
            var app = container.Resolve<Application>();
            app.Run(configuration.InputFile, configuration.OutputFile);
        }

        public static byte[] ReadEmbeddedFile(string resourceName)
        {
            byte[] result;
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                result = Encoding.ASCII.GetBytes(reader.ReadToEnd());
            }

            return result;
        }

        public static IDictionary<Enum, Type> CollectCapabilities()
        {
            return new Dictionary<Enum, Type>
            {
                {CloudLayouterType.ArithmeticSpiral, typeof(CircularCloudLayouter)},
                {ColorScheme.RandomColors, typeof(RandomColorScheme)},
                {FontScheme.Arial, typeof(ArialFontScheme)},
                {SizeScheme.Linear, typeof(LinearSizeScheme)}
            };
        }

        public static IWindsorContainer MakeContainer(Configuration configuration)
        {
            var capabilities = CollectCapabilities();
            var container = new WindsorContainer();

            container.Register(
                GetRegistration<IWordFilter, WordFilterByFile>()
                    .WithArgument("path", configuration.StopWordsFile),
                GetRegistration<IVisualizer, Visualizer>()
                    .WithArgument("backgroundColor", configuration.BackgroundColor)
                    .WithArgument("imageSize", configuration.ImageSize),
                GetRegistration<IWordProcessor, InfinitiveCastProcessor>()
                    .WithArgument("affixFileData", 
                        ReadEmbeddedFile("TagCloudCreator.Dictionaries.Russian.ru_RU.aff"))
                    .WithArgument("dictionaryFileData",
                       ReadEmbeddedFile("TagCloudCreator.Dictionaries.Russian.ru_RU.dic")),
                GetRegistration<IStatisticsCollector, StatisticsCollector>(),
                GetRegistration<IImageSaver, ImageSaver>(),
                GetRegistration<IFileReader, FileReader>(),
                GetRegistration<Point, Point>(),
                GetRegistration<Application, Application>(),
                GetRegistration(typeof(ICloudLayouter), capabilities[configuration.LayouterType]),
                GetRegistration(typeof(IColorScheme), capabilities[configuration.ColorScheme]),
                GetRegistration(typeof(IFontScheme), capabilities[configuration.FontScheme]),
                GetRegistration(typeof(ISizeScheme), capabilities[configuration.SizeScheme]));

            return container;
        }

        public static ComponentRegistration<object> GetRegistration(Type elementFor, Type by)
        {
            return Component.For(elementFor).ImplementedBy(by);
        }

        public static ComponentRegistration<TFor> GetRegistration<TFor, TBy>()
            where TFor : class
            where TBy : TFor
        {
            return Component.For<TFor>().ImplementedBy<TBy>();
        }

        public static Configuration ParseArguments(string[] args)
        {
            var configuration = new Configuration();
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(o => configuration.InputFile = o.Input)
                .WithParsed(o => configuration.OutputFile = o.Output)
                .WithParsed(o => configuration.StopWordsFile = o.Stopwords)
                .WithParsed(o => configuration.BackgroundColor = Color.FromName(o.Background))
                .WithParsed(o => configuration.ImageSize = new Size(o.Width, o.Height))
                .WithParsed(o =>
                {
                    if (o.ColorScheme == "RandomColors")
                        configuration.ColorScheme = ColorScheme.RandomColors;
                    else
                        throw new ArgumentException("Unknown color scheme");
                })
                .WithParsed(o =>
                {
                    if (o.FontScheme == "Arial")
                        configuration.FontScheme = FontScheme.Arial;
                    else
                        throw new ArgumentException("Unknown font scheme");
                })
                .WithParsed(o =>
                {
                    if (o.Layouter == "ArithmeticSpiral")
                        configuration.LayouterType = CloudLayouterType.ArithmeticSpiral;
                    else
                        throw new ArgumentException("Unknown layouter type");
                })
                .WithParsed(o =>
                {
                    if (o.SizeScheme == "Linear")
                        configuration.SizeScheme = SizeScheme.Linear;
                    else
                        throw new ArgumentException("Unknown size scheme");
                })
                .WithNotParsed(o => throw new ArgumentException("Wrong command line arguments"));

            return configuration;
        }
    }
}