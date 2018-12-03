using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.RectanglesLayouter
{
    public interface IRectangleLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
        List<Rectangle> Rectangles { get; }
    }
}