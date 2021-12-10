using CloudLayouter;
using System.Drawing;
using TagsCloudContainer.Abstractions;
using TagsCloudContainer.Defaults.SettingsProviders;

namespace TagsCloudContainer.Defaults;

public class CircularLayouter : ILayouter
{
    private readonly CircularCloudLayouter layouter;

    public CircularLayouter(LayouterSettingsProvider settings) : this(settings.Center)
    {
    }

    protected CircularLayouter(Point center)
    {
        layouter = new(center);
    }

    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        return layouter.PutNextRectangle(rectangleSize);
    }
}
