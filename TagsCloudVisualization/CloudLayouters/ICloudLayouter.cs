using System.Drawing;

namespace TagsCloudVisualization.CloudLayouters;

public interface ICloudLayouter
{
    public IList<Rectangle> Rectangles { get; }
    Rectangle PutNextRectangle(Size rectangleSize);
}
