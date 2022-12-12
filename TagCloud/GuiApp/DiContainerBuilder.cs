using Autofac;
using TagCloud;
using TagsCloudLayouter;

namespace GuiApp;

public static class DiContainerBuilder
{
    public static IContainer Build()
    {
        var builder = new ContainerBuilder();
        builder.RegisterType<ApplicationProperties>().AsSelf().SingleInstance();
        builder.RegisterType<TxtFileLoader>().As<IFileLoader>().SingleInstance();
        builder.RegisterType<WordsParser>().As<IWordsParser>();
        builder.RegisterType<FrequencyDictionary>().AsSelf();
        builder.Register(context => new CircularCloudLayouter(
                context.Resolve<ApplicationProperties>().SizeProperties.ImageCenter,
                context.Resolve<ApplicationProperties>().CloudProperties.Density,
                0.1))
            .As<ICloudLayouter>()
            .InstancePerDependency();
        builder.Register(context => new SizeByFrequency(context.Resolve<ApplicationProperties>().FontProperties)).AsSelf();
        builder.Register(context => new CloudDrawer(
                context.Resolve<ApplicationProperties>().SizeProperties,
                context.Resolve<ApplicationProperties>().Palette))
            .As<ICloudDrawer>();
        builder.RegisterType<WordPreprocessor>().As<IWordPreprocessor>().SingleInstance();
        builder.RegisterType<FrequencyDictionary>().AsSelf().SingleInstance();
        builder.RegisterType<TagCloudForm>().As<Form>().SingleInstance();
        builder.RegisterType<TagCloudConstructor>().As<TagCloudConstructor>();
        return builder.Build();
    }
}