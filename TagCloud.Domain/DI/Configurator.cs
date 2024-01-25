using Autofac;
using TagCloud.Domain.Layout.Interfaces;
using TagCloud.Domain.Layouter;
using TagCloud.Domain.Layouter.Interfaces;
using TagCloud.Domain.Settings;
using TagCloud.Domain.Visualizer.Interfaces;
using TagCloud.Domain.WordProcessing.Extractors;
using TagCloud.Domain.WordProcessing.Interfaces;
using TagCloud.Domain.WordProcessing.WordProcessors;

namespace TagCloud.Domain.DI;

public class Configurator
{
    public static void ConfigureDomain(ContainerBuilder builder)
    {
        ConfigureSettings(builder);
        ConfigureLayout(builder);
        ConfigureVisualizer(builder);
        ConfigureWordProcessing(builder);
    }

    private static void ConfigureSettings(ContainerBuilder builder)
    {
        builder
            .RegisterType<LayoutSettings>()
            .AsSelf()
            .SingleInstance();

        builder
            .RegisterType<PathSettings>()
            .AsSelf()
            .SingleInstance();

        builder
            .RegisterType<TagCloudSettings>()
            .AsSelf()
            .SingleInstance();

        builder
            .RegisterType<VisualizerSettings>()
            .AsSelf()
            .SingleInstance();
    }

    private static void ConfigureLayout(ContainerBuilder builder)
    {
        builder
            .RegisterType<Layout.Layout>()
            .As<ILayout>();

        builder
            .RegisterType<CircularCloudLayouter>()
            .As<ICloudLayouter>()
            .SingleInstance();
    }

    private static void ConfigureVisualizer(ContainerBuilder builder)
    {
        builder
            .RegisterType<Visualizer.Visualizers.Visualizer>()
            .As<IVisualizer>()
            .SingleInstance();
    }

    private static void ConfigureWordProcessing(ContainerBuilder builder)
    {
        builder
            .RegisterAssemblyTypes(typeof(IWordExtractor).Assembly)
            .As<IWordExtractor>()
            .AsImplementedInterfaces()
            .SingleInstance();

        builder
            .RegisterType<WordProcessor>()
            .As<IWordProcessor>()
            .SingleInstance();
    }
}