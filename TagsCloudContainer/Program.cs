using Autofac;
using TagsCloudContainer.ImageCreator;
using TagsCloudContainer.Reader;
using TagsCloudContainer.WordProcessor;
using TagsCloudContainer.WordsToSizesConverter;
using TagsCloudContainer.Layouter;
using TagsCloudContainer.RectanglesTransformer;
using TagsCloudContainer.Visualizer;
using TagsCloudContainer.ImageSaver;
using TagsCloudContainer.ImageSizeCalculator;
using TagsCloudContainer.UI;
using TagsCloudContainer.UI.SettingsCommands;

namespace TagsCloudContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = GetContainer();
            var tagsCloudCreator = container.Resolve<IUserInterface>();
            tagsCloudCreator.Run(args);
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
            builder.RegisterType<ImageSaver.ImageSaver>().As<IImageSaver>().SingleInstance();
            builder.RegisterType<ImageCreator.ImageCreator>().As<IImageCreator>().SingleInstance();
            builder.RegisterType<InputFileSettingsCommand>().As<ISettingsCommand>().SingleInstance();
            builder.RegisterType<OutputFileSettingsCommand>().As<ISettingsCommand>().SingleInstance();
            builder.RegisterType<ImageSizeSettingsCommand>().As<ISettingsCommand>().SingleInstance();
            builder.RegisterType<CyclicConsoleUI>().As<IUserInterface>().SingleInstance();
            return builder.Build();
        }
    }
}
