using System.Drawing;

namespace TagsCloud;

public interface ICloudLayouter
{
    List<Rectangle> Rectangles { get; }
    Rectangle PutNextRectangle(Size rectangleSize);
}