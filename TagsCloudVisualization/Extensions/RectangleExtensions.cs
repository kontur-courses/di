using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization.Extensions
{
    internal static class RectanglesExtensions
    {
        public static bool IntersectsWith(this Rectangle rectangle, IEnumerable<Rectangle> rectangles) =>
            rectangles.Any(rectangle.IntersectsWith);
    }
}