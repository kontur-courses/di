using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloud.Core
{
    public static class RectangleExtensions
    {
        public static bool IntersectsWith(this Rectangle rect, IEnumerable<Rectangle> rectangles)
            => rectangles.Any(rect.IntersectsWith);
    }
}