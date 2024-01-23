using System.Drawing;

namespace TagsCloudVisualization.CloudLayouters;

public interface ICloudLayouter
{
    public Point CloudCenter { get; }
    public IList<Rectangle> Rectangles { get; }
    Rectangle PutNextRectangle(Size rectangleSize);
}
