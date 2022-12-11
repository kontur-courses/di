using System.Drawing;

namespace TagsCloudVisualization.CloudLayouter;

public interface ICloudLayouter
{
    Rectangle PutNextRectangle(Size rectangleSize);
}