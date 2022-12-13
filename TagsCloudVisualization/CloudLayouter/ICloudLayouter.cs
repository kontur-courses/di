using System.Drawing;

namespace TagsCloudVisualization.CloudLayouter;

public interface ICloudLayouter
{
    RectangleF PutNextRectangle(SizeF rectangleSize, LayoutOptions options);

    void Reset();
}