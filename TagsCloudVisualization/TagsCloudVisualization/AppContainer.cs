using System.Drawing;
using Autofac;
using TagsCloudVisualization.CloudDrawer;
using TagsCloudVisualization.TextInput;

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
            .RegisterType<DefaultTextInput>()
            .As<ITextInput>();
        builder
            .RegisterInstance(new CircularCloudLayouterSpiral(new Point(Config.CenterX, Config.CenterY)))
            .As<CircularCloudLayouter>();
        builder
            .RegisterInstance(new DefaultCloudDrawer(Config.WindowWidth, Config.WindowHeight, Config.TextBrush))
            .As<ICloudDrawer>();

        container = builder.Build();
    }

    public static ILifetimeScope GetScope() => container.BeginLifetimeScope();
}