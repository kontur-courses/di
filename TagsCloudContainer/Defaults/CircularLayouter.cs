using Autofac;
using CloudLayouter;
using System.Drawing;
using TagsCloudContainer.Defaults.SettingsProviders;
using TagsCloudVisualization.Abstractions;

namespace TagsCloudContainer.Defaults;

public class CircularLayouter : ILayouter
{
    private readonly CircularCloudLayouter layouter;

    public CircularLayouter(LayouterSettingsProvider settings)
    {
        layouter = new(settings.Center);
    }

    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        return layouter.PutNextRectangle(rectangleSize);
    }

    [Register]
    public static void Register(ContainerBuilder builder)
    {
        builder.RegisterType<CircularLayouter>().AsSelf().As<ILayouter>();
    }
}
