using Autofac;
using TagsCloudCore.Drawing;
using TagsCloudCore.Drawing.Colorers;
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

        containerBuilder.RegisterAssemblyTypes(typeof(IWordColorer).Assembly).As<IWordColorer>().SingleInstance();
        
        containerBuilder.RegisterType<DefaultImageDrawer>().As<IImageDrawer>().SingleInstance();
        
        containerBuilder
            .RegisterInstance(new DefaultWordFilter(new TxtFileWordParser(AppSettings.PathToBoringWordsFilter)))
            .As<IWordFilter>()
            .SingleInstance();
        
        containerBuilder.RegisterType<DefaultWordProcessor>().As<IProcessedWordProvider>().SingleInstance();
        
        containerBuilder.RegisterType<DefaultWordCloudDistributor>().As<IWordCloudDistributorProvider>()
            .SingleInstance();

        return containerBuilder;
    }
}