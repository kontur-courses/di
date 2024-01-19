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
        
        containerBuilder.RegisterType<DefaultWordFiler>()
            .Named<IWordProvider>("filtered_words")
            .WithParameter((pi, _) => pi.Name == "words", (_, c) => c.Resolve<IWordProvider>())
            .WithParameter((pi, _) => pi.Name == "wordsToExclude", (_, c) => c.ResolveNamed<IWordProvider>("filter"))
            .SingleInstance();

        containerBuilder.RegisterType<DefaultWordGrouper>()
            .As<IWordGrouperProvider>()
            .WithParameter((pi, _) => pi.Name == "words", (_, c) => c.ResolveNamed<IWordProvider>("filtered_words").Words)
            .SingleInstance();

        containerBuilder.RegisterType<CircularCloudLayouter>()
            .As<ICloudLayouter>()
            .WithParameter("center", new Point(0, 0))
            .SingleInstance();

        containerBuilder.RegisterType<DefaultOptionsProvider>()
            .As<IOptionsProvider>()
            .SingleInstance();

        containerBuilder.RegisterType<DefaultWordCloudDistributor>()
            .As<IWordCloudDistributorProvider>()
            .SingleInstance();

        containerBuilder.RegisterType<DefaultImageDrawer>()
            .As<IImageDrawer>()
            .SingleInstance();

        containerBuilder
            .RegisterInstance(new TxtFileWordParser("words.txt"))
            .As<IWordProvider>()
            .SingleInstance();

        containerBuilder
            .RegisterInstance(new TxtFileWordParser("filter.txt"))
            .Named<IWordProvider>("filter")
            .SingleInstance();
        
        var containter = containerBuilder.Build();

        var drawer = containter.Resolve<IImageDrawer>();

        var bitmap = drawer.DrawImage();
        DefaultImageDrawer.SaveImage(bitmap);
    }
}