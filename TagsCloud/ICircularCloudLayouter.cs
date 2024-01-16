using System.Drawing;

namespace TagsCloud;

public interface ICircularCloudLayouter
{
    List<Rectangle> Rectangles { get; }
    Rectangle PutNextRectangle(Size rectangleSize);
}