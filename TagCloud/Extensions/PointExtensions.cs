using System.Drawing;
using System.Linq;

namespace TagCloud.Extensions
{
    public static class PointExtensions
    {
        public static Rectangle GetBounds(this Point[] points)
        {
            var minX = points.Select(r => r.X).Min();
            var maxX = points.Select(r => r.X).Max();
            var minY = points.Select(r => r.Y).Min();
            var maxY = points.Select(r => r.Y).Max();
            return new Rectangle(
                new Point(minX, minY),
                new Size(maxX - minX + 1, maxY - minY + 1));
        }
    }
}