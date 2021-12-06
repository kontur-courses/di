using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.Layouting
{
    public interface ILayouter
    {
        List<Rectangle> GetRectanglesCopy();

        Size GetRectanglesBoundaryBox();

        Rectangle PutNextRectangle(Size rectangleSize);
    }
}