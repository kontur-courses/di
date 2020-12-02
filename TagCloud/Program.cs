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

namespace TagCloud
{
    class Program
    {
        static void Main(string[] args)
        {
            var containerBuilder = new ContainerBuilder();
            // Временно константы зарегистрировал
            containerBuilder.Register(_ => new Point(1000, 1000));
            containerBuilder.Register(_ => new Size(2000, 2000));
            containerBuilder.RegisterInstance(new FileTextReaderSettings("C:/shpora/food.txt"))
                .As<FileTextReaderSettings>();
            containerBuilder.RegisterInstance(new DrawerSettings(Color.Black))
                .As<DrawerSettings>();
            containerBuilder.RegisterInstance(new LayoutSettings(FontFamily.GenericMonospace, 14, 48))
                .As<LayoutSettings>();
            containerBuilder.RegisterInstance(new SaverSettings(null, "foodaaa"))
                .As<SaverSettings>();
            containerBuilder.RegisterType<WordNormalizer>().As<IWordNormalizer>();
            containerBuilder.RegisterType<StandardAnalyzer>().As<ITextAnalyzer>();
            containerBuilder.RegisterType<CircularCloudLayouter>().As<IRectangleLayouter>();
            containerBuilder.RegisterType<TagCloudLayout>().As<ITagCloudLayout>();
            containerBuilder.RegisterType<FileTextReader>().As<ITextReader>();
            containerBuilder.RegisterType<BmpSaver>().As<IImageSaver>();
            containerBuilder.RegisterType<TagDrawer>().As<ITagDrawer>();
            containerBuilder.RegisterType<TagCloud>().AsSelf();

            var container = containerBuilder.Build();
            
            var tagCloud = container.Resolve<TagCloud>();
            tagCloud.MakeTagCloud();
        }
    }
}