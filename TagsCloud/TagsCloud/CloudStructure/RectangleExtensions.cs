using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud.CloudStructure
{
    public static class RectangleExtensions
    {
        public static bool IntersectsWithAny(this Rectangle rectangle, IEnumerable<Rectangle> rectangles)
        {
            foreach (var rect in rectangles)
                if (rectangle.IntersectsWith(rect))
                    return true;
            return false;
        }
    }
}
