using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using TagCloud;
using TagCloud.Enums;
using TagCloud.Interfaces;
using TagCloud.Layouter;
using TagCloud.Visualizer;

namespace TagCloudCreator
{
    public static class ContainerBuilder
    {
        public static IWindsorContainer ConfigureContainer(Configuration configuration)
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

        public static byte[] ReadEmbeddedFile(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
                return Encoding.ASCII.GetBytes(reader.ReadToEnd());
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