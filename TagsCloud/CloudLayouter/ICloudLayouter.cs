using System.Drawing;

namespace TagsCloud.CloudLayouter;

public interface ICloudLayouter
{
    List<Rectangle> Rectangles { get; }
    Rectangle PutNextRectangle(Size rectangleSize);
}