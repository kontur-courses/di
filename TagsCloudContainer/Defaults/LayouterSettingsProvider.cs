using CloudLayouter;
using System.Drawing;
using TagsCloudVisualization.Abstractions;

namespace TagsCloudContainer.Defaults;

public class LayouterSettingsProvider : ISettingsProvider
{
    public Point Center { get; set; } = new Point(300, 100);

    public CircularCloudLayouter Create()
    {
        return new(Center);
    }
}