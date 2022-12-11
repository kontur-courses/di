using Autofac;
using TagCloud;
using TagsCloudLayouter;

namespace ConsoleApp;

public static class DiContainerBuilder
{
    public static IContainer Build()
    {
        var builder = new ContainerBuilder();
        RegisterProperties(builder);
        builder.RegisterType<TxtFileLoader>().As<IFileLoader>().SingleInstance();
        builder.RegisterType<WordsParser>().As<IWordsParser>();
        builder.RegisterType<FrequencyDictionary>().AsSelf();
        builder.Register(context => new CircularCloudLayouter(
            context.Resolve<SizeProperties>().ImageCenter, 
            context.Resolve<CloudProperties>().Density, 
            0.1))
            .As<ICloudLayouter>()
            .InstancePerDependency();
        builder.RegisterType<TextWrapper>().AsSelf();
        builder.RegisterType<CloudDrawer>().As<ICloudDrawer>();
        builder.RegisterType<TagCloudConstructor>().As<TagCloudConstructor>();
        return builder.Build();
    }

    private static void RegisterProperties(ContainerBuilder builder)
    {
        builder.RegisterType<ApplicationProperties>().AsSelf().SingleInstance();
        builder.Register(context => context.Resolve<ApplicationProperties>().Palette).AsSelf().SingleInstance();
        builder.Register(context => context.Resolve<ApplicationProperties>().SizeProperties).AsSelf().SingleInstance();
        builder.Register(context => context.Resolve<ApplicationProperties>().FontProperties).AsSelf().SingleInstance();
        builder.Register(context => context.Resolve<ApplicationProperties>().CloudProperties).AsSelf().SingleInstance();
    }
}