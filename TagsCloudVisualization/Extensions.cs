using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public static class RectangleExtensions
    {
        public static bool IntersectsWith(this Rectangle rectangle, IEnumerable<Rectangle> rectangles)
            => rectangles.Any(rectangle.IntersectsWith);

        public static Point Center(this Rectangle rectangle)
            => rectangle.Location + rectangle.Size / 2;
    }

    public static class PointExtensions
    {
        public static int GetDistanceSquareTo(this Point original, Point point)
        {
            var dx = point.X - original.X;
            var dy = point.Y - original.Y;
            return dx * dx + dy * dy;
        }
    }
}
