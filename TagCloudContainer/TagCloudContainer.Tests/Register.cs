using Autofac;
using TagCloudContainer.Additions;
using TagCloudContainer.Additions.Interfaces;

namespace TagCloudContainer.Tests;

public static class Register
{
    public static IContainer Registry(TagCloudContainerConfig tagCloudContainerConfig, TagCloudFormConfig tagCloudFormConfig, WordReaderConfig wordReaderConfig)
    {
        var builder = new ContainerBuilder();

        builder.RegisterInstance(tagCloudContainerConfig).As<ITagCloudContainerConfig>();
        builder.RegisterInstance(tagCloudFormConfig).As<ITagCloudFormConfig>();
        builder.RegisterInstance(wordReaderConfig).As<IWordReaderConfig>();
        
        builder.RegisterType<ImageCreator>().As<IImageCreator>().SingleInstance();
        builder.RegisterType<WordValidator>().As<IWordValidator>().SingleInstance();
        builder.RegisterType<TagCloudPlacer>().As<ITagCloudPlacer>().SingleInstance();
        builder.RegisterType<WordsReader>().As<IWordsReader>().SingleInstance();
        builder.RegisterType<TagCloudProvider>().As<ITagCloudProvider>().SingleInstance();
        
        return builder.Build();
    }
}
