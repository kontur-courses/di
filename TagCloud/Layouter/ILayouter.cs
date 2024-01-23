using System.Drawing;

namespace TagCloud.Layouter;

public interface ILayouter
{
    IList<Rectangle> Rectangles { get; }
    Rectangle PutNextRectangle(Size rectangleSize);
}