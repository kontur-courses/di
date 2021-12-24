using System.Collections.Generic;
using System.Drawing;
using ResultProject;

namespace TagsCloudVisualization.Printing
{
    internal interface IRectanglesReCalculator
    {
        Result<IList<Rectangle>> RecalculateRectangles(IList<Rectangle> rectangles, Size defaultMaxSize);
        Result<IList<Rectangle>> MoveToCenter(IList<Rectangle> rectangles);
    }
}