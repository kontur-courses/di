using Autofac;
using TagsCloudCore.Drawing;
using TagsCloudCore.TagCloudForming;
using TagsCloudCore.WordProcessing.WordFiltering;
using TagsCloudCore.WordProcessing.WordGrouping;
using TagsCloudCore.WordProcessing.WordInput;

namespace TagsCloudCore.Common;

public static class DiContainerBuilder
{
    public static ContainerBuilder RegisterDefaultDependencies()
    {
        var containerBuilder = new ContainerBuilder();
        
        containerBuilder.RegisterType<DefaultImageDrawer>().As<IImageDrawer>().SingleInstance();
        containerBuilder.RegisterInstance(new DefaultWordFilter(new TxtFileWordParser(AppSettings.PathToBoringWordsFilter)))
            .As<IWordFilter>()
            .SingleInstance();
        containerBuilder.RegisterType<DefaultWordProcessor>().As<IProcessedWordProvider>().SingleInstance();
        containerBuilder.RegisterType<DefaultWordCloudDistributor>().As<IWordCloudDistributorProvider>().SingleInstance();
        containerBuilder.RegisterType<DefaultImageDrawer>().As<IImageDrawer>().SingleInstance();

        return containerBuilder;
    } 
}