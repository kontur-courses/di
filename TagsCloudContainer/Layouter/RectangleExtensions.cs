using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public static class RectangleExtensions
    {
        public static bool IntersectsWithAnyFrom(this Rectangle rect, IEnumerable<Rectangle> rectangles) =>
            rectangles.Any(currentRectangle => currentRectangle.IntersectsWith(rect));
    }
}