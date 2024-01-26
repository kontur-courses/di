using Autofac;
using TagsCloudContainer.Drawing;
using TagsCloudContainer.TagCloudForming;
using TagsCloudContainer.WordProcessing.WordFiltering;
using TagsCloudContainer.WordProcessing.WordGrouping;
using TagsCloudContainer.WordProcessing.WordInput;

namespace TagsCloudContainer.Common;

public static class DiContainerBuilder
{
    public static ContainerBuilder RegisterDefaultDependencies()
    {
        var containerBuilder = new ContainerBuilder();
        
        containerBuilder.RegisterType<DefaultImageDrawer>().As<IImageDrawer>().SingleInstance();
        containerBuilder.RegisterInstance(new DefaultWordFilter(new TxtFileWordParser("../../../../TagsCloudContainer/Resources/filter.txt")))
            .As<IWordFilter>()
            .SingleInstance();
        containerBuilder.RegisterType<DefaultWordProcessor>().As<IProcessedWordProvider>().SingleInstance();
        containerBuilder.RegisterType<DefaultWordCloudDistributor>().As<IWordCloudDistributorProvider>().SingleInstance();
        containerBuilder.RegisterType<DefaultImageDrawer>().As<IImageDrawer>().SingleInstance();

        return containerBuilder;
    } 
}