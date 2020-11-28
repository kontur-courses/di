using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CloudLayouter.SpecialMethods
{
    public static class RectanglesIntersection
    {
        public static bool IsAnyIntersectWithRectangles(Rectangle rectangleToCheck, List<Rectangle> rectangles) =>
            rectangles.Any(rec => rec.IntersectsWith(rectangleToCheck));
    }
}