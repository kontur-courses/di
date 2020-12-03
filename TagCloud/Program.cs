using Autofac;
using TagCloud.Drawers;
using TagCloud.ImageSavers;
using TagCloud.TextReaders;
using TagCloud.Layouters;
using TagCloud.Settings;
using TagCloud.TextAnalyzer;
using TagCloud.TextAnalyzer.WordNormalizer;
using TagCloud.UserInterfaces;

namespace TagCloud
{
    class Program
    {
        static void Main(string[] args)
        {
            var cli = new ConsoleLineInterface(args);
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterInstance(cli.FileReaderSettings)
                .As<FileReaderSettings>();
            containerBuilder.RegisterInstance(cli.LayouterSettings)
                .As<CircularLayouterSettings>();
            containerBuilder.RegisterInstance(cli.DrawerSettings)
                .As<DrawerSettings>();
            containerBuilder.RegisterInstance(cli.SaverSettings)
                .As<SaverSettings>();
            containerBuilder.RegisterType<WordNormalizer>().As<IWordNormalizer>();
            containerBuilder.RegisterType<StandardAnalyzer>().As<ITextAnalyzer>();
            containerBuilder.RegisterType<CircularCloudLayouter>().As<IRectangleLayouter>();
            containerBuilder.RegisterType<FileReader>().As<ITextReader>();
            containerBuilder.RegisterType<ImageSaver>().As<IImageSaver>();
            containerBuilder.RegisterType<TagDrawer>().As<ITagDrawer>();
            containerBuilder.RegisterType<TagCloud>().AsSelf();

            var container = containerBuilder.Build();
            
            var tagCloud = container.Resolve<TagCloud>();
            tagCloud.MakeTagCloud();
        }
    }
}