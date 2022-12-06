using System.Drawing;
using Autofac;
using TagsCloudVisualization.CloudDrawer;

namespace TagsCloudVisualization;

public static class AppContainer
{
    private static IContainer container;

    public static void Configure()
    {
        var builder = new ContainerBuilder();
        builder.RegisterType<DefaultPreprocessor>().As<IPreprocessor>();
        builder.RegisterType<DefaultCloudGenerator>().As<ICloudGenerator>();
        builder
            .RegisterInstance(new CircularCloudLayouterSpiral(new Point(50, 50)))
            .As<CircularCloudLayouter>();
        builder
            .RegisterInstance(new DefaultCloudDrawer(100, 100, 1, Color.Black))
            .As<ICloudDrawer>();
        container = builder.Build();
    }

    public static ILifetimeScope GetScope() => container.BeginLifetimeScope();
}