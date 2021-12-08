using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer.TagsCloudVisualization
{
    public static class RectangleExtensions
    {
        public static bool IntersectsWithRectangles(this Rectangle rectangle, List<Rectangle> rectangles)
        {
            return rectangles != null && rectangles.Any(r => r.IntersectsWith(rectangle));
        }
    }
}