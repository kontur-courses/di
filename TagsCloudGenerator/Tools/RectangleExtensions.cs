using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudGenerator.Tools
{
    public static class RectangleExtensions
    {
        public static IEnumerable<Point> Vertexes(this Rectangle rectangle)
        {
            yield return rectangle.Location;
            yield return rectangle.Location + new Size(rectangle.Width, 0);
            yield return rectangle.Location + rectangle.Size;
            yield return rectangle.Location + new Size(0, rectangle.Height);
        }

        public static double Distance(this Rectangle r1, Rectangle r2)
        {
            var points1 = r1.Vertexes().ToList();
            var points2 = r2.Vertexes().ToList();
            var min = points1[0].Distance(points2[0]);

            return points1.Aggregate(min, (current, p1) => points2
                .Select(p2 => p1.Distance(p2))
                .Concat(new[] {current})
                .Min());
        }
    }
}