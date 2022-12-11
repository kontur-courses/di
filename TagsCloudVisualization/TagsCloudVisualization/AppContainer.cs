using System.Drawing;
using Autofac;
using TagsCloudVisualization.Clients;
using TagsCloudVisualization.CloudDrawer;
using TagsCloudVisualization.TextInput;

namespace TagsCloudVisualization;

public static class AppContainer
{
    private static IContainer container;

    public static void Configure(Options options)
    {
        var builder = new ContainerBuilder();
        builder.RegisterType<DefaultPreprocessor>().As<IPreprocessor>();
        builder.RegisterType<DefaultCloudGenerator>().As<ICloudGenerator>();
        builder.RegisterInstance(new Font(options.Font, options.FontSize)).As<Font>();

        builder
            .RegisterInstance(new DefaultTextInput(options.Path))
            .As<ITextInput>();
        builder
            .RegisterInstance(new CircularCloudLayouterSpiral(new Point(Config.CenterX, Config.CenterY)))
            .As<CircularCloudLayouter>();
        builder
            .RegisterInstance(
                new DefaultCloudDrawer(
                    options.ImageWidth,
                    options.ImageHeight,
                    options.Colors)
            )
            .As<ICloudDrawer>();

        container = builder.Build();
    }

    public static ILifetimeScope GetScope() => container.BeginLifetimeScope();
}