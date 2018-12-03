using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization.Utils
{
    public static class RectangleExtension
    {
        public static Point GetCenter(this Rectangle rectangle)
        {
            int xCenter = rectangle.X + rectangle.Width / 2;
            int yCenter = rectangle.Y + rectangle.Height / 2;
            return new Point(xCenter, yCenter);
        }

        public static bool IntersectsWithAny(this Rectangle rectangle, IEnumerable<Rectangle> otherRectangles)
        {
            return otherRectangles.Any(rectangle.IntersectsWith);
        }
    }
}
