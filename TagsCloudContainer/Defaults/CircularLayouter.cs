using Autofac;
using CloudLayouter;
using System.Drawing;
using TagsCloudContainer.Abstractions;
using TagsCloudContainer.Defaults.SettingsProviders;
using TagsCloudContainer.Registrations;

namespace TagsCloudContainer.Defaults;

public class CircularLayouter : ILayouter
{
    private readonly CircularCloudLayouter layouter;

    public CircularLayouter(LayouterSettingsProvider settings) : this(settings.Center)
    {
    }

    public CircularLayouter(Point center)
    {
        layouter = new(center);
    }

    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        return layouter.PutNextRectangle(rectangleSize);
    }

    [Register]
    public static void Register(ContainerBuilder builder)
    {
        builder.RegisterType<CircularLayouter>().AsSelf().As<ILayouter>()
            .UsingConstructor(typeof(LayouterSettingsProvider));
    }
}
