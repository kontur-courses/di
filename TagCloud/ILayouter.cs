using System.Collections.Generic;
using System.Drawing;

namespace TagCloud
{
    public interface ILayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
        List<Rectangle> Rectangles { get; }
    }
}