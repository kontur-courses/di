using System.Collections.Generic;
using System.Drawing;

namespace TagCloud
{
    public interface ITagCloud
    {
        Rectangle PutNextRectangle(Size rectangleSize);
        List<Rectangle> Rectangles { get; }
    }
}