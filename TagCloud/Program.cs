using System.Drawing;
using Autofac;
using TagCloud.Drawers;
using TagCloud.ImageSavers;
using TagCloud.Layout;
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
            var cui = new ConsoleUserInterface(args);
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterInstance(cui.FileReaderSettings)
                .As<FileReaderSettings>();
            containerBuilder.RegisterInstance(cui.CircularLayouterSettings)
                .As<CircularLayouterSettings>();
            containerBuilder.RegisterInstance(cui.DrawerSettings)
                .As<DrawerSettings>();
            containerBuilder.RegisterInstance(cui.LayoutSettings)
                .As<LayoutSettings>();
            containerBuilder.RegisterInstance(cui.SaverSettings)
                .As<SaverSettings>();
            containerBuilder.RegisterType<WordNormalizer>().As<IWordNormalizer>();
            containerBuilder.RegisterType<StandardAnalyzer>().As<ITextAnalyzer>();
            containerBuilder.RegisterType<CircularCloudLayouter>().As<IRectangleLayouter>();
            containerBuilder.RegisterType<TagCloudLayout>().As<ITagCloudLayout>();
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