using Autofac;
using TagCloud.Analyzers;
using TagCloud.Creators;
using TagCloud.Layouters;
using TagCloud.Readers;
using TagCloud.UI;
using TagCloud.UI.Console;
using TagCloud.Visualizers;
using TagCloud.Writers;

namespace TagCloud
{
    public static class DependencyConfigurator
    {
        public static IContainer GetConfiguredContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<FileReaderFactory>().As<IFileReaderFactory>().SingleInstance();
            builder.RegisterType<TextAnalyzer>().As<ITextAnalyzer>().SingleInstance();
            builder.RegisterType<FrequencyAnalyzer>().As<IFrequencyAnalyzer>().SingleInstance();
            builder.RegisterType<TagCreatorFactory>().As<ITagCreatorFactory>().SingleInstance();
            builder.RegisterType<CircularCloudLayouterFactory>().As<ICloudLayouterFactory>().SingleInstance();
            builder.RegisterType<CloudVisualizer>().As<IVisualizer>().SingleInstance();
            builder.RegisterType<BitmapWriter>().As<IFileWriter>().SingleInstance();
            builder.RegisterType<WordsToLowerConverter>().As<IWordConverter>().SingleInstance();
            builder.RegisterType<BoringWordsFilter>()
                .As<IWordFilter>()
                .As<BoringWordsFilter>()
                .SingleInstance();
            builder.RegisterType<TagColoringFactory>().As<ITagColoringFactory>().SingleInstance();

            builder.RegisterType<ConsoleUI>().As<IUserInterface>().SingleInstance();
            return builder.Build();
        }
    }
}
