using System.Drawing;
using Autofac;
using TagsCloudContainer.Drawing;
using TagsCloudContainer.DrawingOptions;
using TagsCloudContainer.TagCloudForming;
using TagsCloudContainer.WordProcessing.WordFiltering;
using TagsCloudContainer.WordProcessing.WordGrouping;
using TagsCloudContainer.WordProcessing.WordInput;
using TagsCloudVisualization;

namespace TagsCloudContainer;

public static class Program
{
    public static void Main(string[] args)
    {
        var containerBuilder = new ContainerBuilder();

        containerBuilder
            .RegisterInstance(new TxtFileWordParser("words.txt"))
            .As<IWordProvider>()
            .SingleInstance();
        
        containerBuilder
            .RegisterInstance(new DefaultWordFilter(new TxtFileWordParser("filter.txt")))
            .As<IWordFilter>()
            .SingleInstance();

        containerBuilder.RegisterType<DefaultWordProcessor>()
            .As<IProcessedWordProvider>()
            .SingleInstance();

        containerBuilder.RegisterInstance(new CircularCloudLayouter(Point.Empty))
            .As<ICloudLayouter>()
            .SingleInstance();
        
        containerBuilder.RegisterType<DefaultWordCloudDistributor>()
            .As<IWordCloudDistributorProvider>()
            .SingleInstance();

        containerBuilder.RegisterType<DefaultImageDrawer>()
            .As<IImageDrawer>()
            .SingleInstance();

        containerBuilder.RegisterType<DefaultOptionsProvider>()
            .As<IOptionsProvider>()
            .SingleInstance();
        
        var containter = containerBuilder.Build();

        var drawer = containter.Resolve<IImageDrawer>();

        var bitmap = drawer.DrawImage();
        DefaultImageDrawer.SaveImage(bitmap);
    }
}