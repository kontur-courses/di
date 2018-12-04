using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization
{
    public static class RectangleExtensions
    {
        public static bool IsIntersectsWithAnyRect(this Rectangle rectangle, IEnumerable<Rectangle> rectangles)
            => rectangles.Any(r => r.Intersects(rectangle));
    }
}
