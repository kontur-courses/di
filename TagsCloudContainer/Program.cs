using Autofac;
using TagsCloudContainer.Reader;
using TagsCloudContainer.WordProcessor;
using TagsCloudContainer.WordsToSizesConverter;
using TagsCloudContainer.Layouter;
using TagsCloudContainer.RectanglesTransformer;
using TagsCloudContainer.Visualizer;
using TagsCloudContainer.ImageSaver;
using TagsCloudContainer.ImageSizeCalculator;
using TagsCloudContainer.Ui;

namespace TagsCloudContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = GetContainer();
            var tagsCloudCreator = container.Resolve<ConsoleTagsCloudCreator>();
            tagsCloudCreator.CreateImage(args);
        }

        private static IContainer GetContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<FileReader>().As<ITextReader>().SingleInstance();
            builder.RegisterType<BasicWordProcessor>().As<IWordProcessor>().SingleInstance();
            builder.RegisterType<WordFrequenciesToSizesConverter>().As<IWordFrequenciesToSizesConverter>().SingleInstance();
            builder.RegisterType<DefaultLayouterSettings>().As<ILayouterSettings>().SingleInstance();
            builder.RegisterType<CircularCloudLayouter>().As<ILayouter>().SingleInstance();
            builder.RegisterType<DefaultImageSizeCalculator>().As<IImageSizeCalculator>();
            builder.RegisterType<CenterRectanglesShifter>().As<IRectanglesTransformer>().SingleInstance();
            builder.RegisterType<DefaultVisualizerSettings>().As<IVisualizerSettings>().SingleInstance();
            builder.RegisterType<TagCloudVisualizer>().As<IVisualizer>().SingleInstance();
            builder.RegisterType<Saver>().As<IImageSaver>().SingleInstance();
            builder.RegisterType<ConsoleTagsCloudCreator>().AsSelf().SingleInstance();
            return builder.Build();
        }
    }
}
