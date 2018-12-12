using System;
using System.IO;
using System.Reflection;
using System.Text;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using TagCloud;
using TagCloud.Interfaces;
using TagCloud.Layouter;
using TagCloud.Visualizer;

namespace TagCloudCreator
{
    public static class ContainerBuilder
    {
        public static IWindsorContainer ConfigureContainer(Configuration configuration)
        {
            var layouters = TypesCollector.CollectLayouters();
            var colorSchemes = TypesCollector.CollectColorSchemes();
            var fontSchemes = TypesCollector.CollectFontSchemes();
            var sizeSchemes = TypesCollector.CollectSizeSchemes();
            var container = new WindsorContainer();

            container.Register(
                GetRegistration<IWordFilter, WordFilter>()
                    .WithArgument("path", configuration.StopWordsFile)
                    .WithArgument("ignoreBoring", configuration.IgnoreBoring),
                GetRegistration<IVisualizer, Visualizer>()
                    .WithArgument("backgroundColor", configuration.BackgroundColor)
                    .WithArgument("imageSize", configuration.ImageSize),
                GetRegistration<IWordProcessor, InfinitiveCastProcessor>()
                    .WithArgument("affixFileData",
                        ReadEmbeddedFile("TagCloudCreator.Dictionaries.Russian.ru_RU.aff"))
                    .WithArgument("dictionaryFileData",
                        ReadEmbeddedFile("TagCloudCreator.Dictionaries.Russian.ru_RU.dic")),
                GetRegistration<IStatisticsCollector, StatisticsCollector>(),
                GetRegistration<ICloudLayouter, SpiralCloudLayouter>(),
                GetRegistration<IImageSaver, ImageSaver>(),
                GetRegistration<IFileReader, FileReader>(),
                GetRegistration<Point, Point>(),
                GetRegistration<Application, Application>(),
                GetRegistration(typeof(ISpiral), layouters[configuration.LayouterType]),
                GetRegistration(typeof(IColorScheme), colorSchemes[configuration.ColorScheme]),
                GetRegistration(typeof(IFontScheme), fontSchemes[configuration.FontScheme]),
                GetRegistration(typeof(ISizeScheme), sizeSchemes[configuration.SizeScheme]));

            return container;
        }

        public static byte[] ReadEmbeddedFile(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                return Encoding.ASCII.GetBytes(reader.ReadToEnd());
            }
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
    }
}