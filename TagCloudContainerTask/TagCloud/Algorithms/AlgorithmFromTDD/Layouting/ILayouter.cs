using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.Algorithms.AlgorithmFromTDD.Layouting
{
    public interface ILayouter
    {
        List<Rectangle> GetRectanglesCopy();

        Size GetRectanglesBoundaryBox();

        Rectangle PutNextRectangle(Size rectangleSize);
    }
}