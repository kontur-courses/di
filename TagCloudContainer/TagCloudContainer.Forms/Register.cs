using Autofac;
using TagCloudContainer.Configs;
using TagCloudContainer.Core;
using TagCloudContainer.Core.Interfaces;
using TagCloudContainer.Core.Utils;
using TagCloudContainer.Forms.Interfaces;
using TagCloudContainer.Forms.Validators;

namespace TagCloudContainer.Forms;

public static class Register
{
    public static IContainer Registry()
    {
        var builder = new ContainerBuilder();

        builder.RegisterType<TagCloud>();
        builder.RegisterType<Settings>();

        var tagCloudContainerConfig = new TagCloudContainerConfig();

        builder.RegisterInstance(tagCloudContainerConfig).As<ITagCloudContainerConfig>();
        builder.RegisterInstance(tagCloudContainerConfig).As<ITagCloudFormConfig>();

        builder.RegisterType<TagCloudContainerConfigValidator>().As<IConfigValidator<ITagCloudContainerConfig>>();
        builder.RegisterType<TagCloudFormConfigValidator>().As<IConfigValidator<ITagCloudFormConfig>>();
        builder.RegisterType<ImageCreator>().As<IImageCreator>().SingleInstance();
        builder.RegisterType<SizeInvestigator>().As<ISizeInvestigator>().SingleInstance();
        builder.RegisterType<WordValidator>().As<IWordValidator>().SingleInstance();
        builder.RegisterType<TagCloudPlacer>().As<ITagCloudPlacer>().SingleInstance();
        builder.RegisterType<WordsReader>().As<IWordsReader>().SingleInstance();
        builder.RegisterType<TagCloudProvider>().As<ITagCloudProvider>().SingleInstance();
        
        return builder.Build();
    }
}