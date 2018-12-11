using System.Drawing;
using System.Linq;

namespace TagCloud.Extensions
{
    public static class PointExtensions
    {
        public static Rectangle GetBounds(this Point[] points)
        {
            var minX = points.Min(r => r.X);
            var maxX = points.Max(r => r.X);
            var minY = points.Min(r => r.Y);
            var maxY = points.Max(r => r.Y);
            return new Rectangle(
                new Point(minX, minY),
                new Size(maxX - minX + 1, maxY - minY + 1));
        }
    }
}