using System.Drawing;
using Autofac;
using TagsCloudVisualization.CloudDrawer;
using TagsCloudVisualization.TextInput;

namespace TagsCloudVisualization;

public static class AppContainer
{
    private const int
        WINDOW_WIDTH = 600,
        WINDOW_HEIGHT = 400,
        CENTER_X = 300,
        CENTER_Y = 200;

    private const string
        DEFAULT_PATH = "..\\..\\..\\input.txt";

    private static IContainer container;

    public static void Configure()
    {
        var builder = new ContainerBuilder();
        builder.RegisterType<DefaultPreprocessor>().As<IPreprocessor>();
        builder.RegisterType<DefaultCloudGenerator>().As<ICloudGenerator>();

        builder
            .RegisterInstance(new DefaultTextInput(DEFAULT_PATH))
            .As<ITextInput>();
        builder
            .RegisterInstance(new CircularCloudLayouterSpiral(new Point(CENTER_X, CENTER_Y)))
            .As<CircularCloudLayouter>();
        builder
            .RegisterInstance(new DefaultCloudDrawer(WINDOW_WIDTH, WINDOW_HEIGHT, 1, Color.Black))
            .As<ICloudDrawer>();

        container = builder.Build();
    }

    public static ILifetimeScope GetScope() => container.BeginLifetimeScope();
}