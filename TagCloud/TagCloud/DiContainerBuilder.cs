using Autofac;
using TagsCloudLayouter;

namespace TagCloud;

public static class DiContainerBuilder
{
    public static IContainer Build()
    {
        var builder = new ContainerBuilder();
        RegisterProperties(builder);
        
        builder.RegisterType<WordsParser>().As<IWordsParser>();
        builder.RegisterType<FrequencyDictionary>().AsSelf();
        builder.Register(context => new CircularCloudLayouter(
            context.Resolve<SizeProperties>().ImageCenter, 
            context.Resolve<CloudProperties>().Density, 
            context.Resolve<CloudProperties>().AngleStep))
            .As<ICloudLayouter>()
            .InstancePerDependency();
        builder.RegisterType<TextWrapper>().AsSelf();
        builder.RegisterType<CloudDrawer>().As<ICloudDrawer>();
        return builder.Build();
    }

    private static void RegisterProperties(ContainerBuilder builder)
    {
        builder.RegisterType<TxtFileLoader>().As<IFileLoader>().SingleInstance();
        builder.RegisterInstance(new Palette(Color.Tan, Color.Teal)).AsSelf().SingleInstance();
        builder.RegisterInstance(new SizeProperties()).AsSelf().SingleInstance();
        builder.RegisterType<FontProperties>().AsSelf().SingleInstance();
        builder.Register(context => new CloudProperties
            {
                Center = context.Resolve<SizeProperties>().ImageCenter, 
                Density = 0.1, 
                AngleStep = 0.1
            })
            .As<CloudProperties>()
            .SingleInstance();
    }
}