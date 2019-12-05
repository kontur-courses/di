using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TagsCloudContainer.RectanglesShifter
{
    public interface IRectanglesShifter
    {
        IList<Rectangle> ShiftRectangles(IList<Rectangle> rectangles, Point oldCenter);
    }
}
