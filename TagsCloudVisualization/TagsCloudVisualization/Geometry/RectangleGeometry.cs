using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization.Geometry
{
    public static class RectangleGeometry
    {
        public static IEnumerable<Rectangle> GetCornerRectangles(Size size, HashSet<Point> points)
        {
            foreach (var point in points)
            {
                yield return  new Rectangle(new Point(point.X - size.Width / 2, point.Y - size.Height / 2), size);
                yield return new Rectangle(point, size);
                yield return new Rectangle(new Point(point.X - size.Width, point.Y), size);
                yield return new Rectangle(new Point(point.X - size.Width, point.Y - size.Height), size);
                yield return new Rectangle(new Point(point.X, point.Y - size.Height), size);
            }
        }

        public static IEnumerable<Point> GetCorners(this Rectangle rectangle)
        {
            yield return rectangle.Location;
            yield return new Point(rectangle.Right, rectangle.Y);
            yield return new Point(rectangle.X, rectangle.Bottom);
            yield return new Point(rectangle.Right, rectangle.Bottom);
        }

        public static bool AnyRectanglePointOutOfRange(this Rectangle rectangle, Point point, 
            double permissionRange)
        {
            return rectangle.GetCorners().ToList().Any(c => c.DistanceTo(point) > permissionRange);
        }

        public static double Square(this Rectangle rectangle) => rectangle.Width * rectangle.Height;
    }
}
