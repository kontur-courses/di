using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer.Layouter
{
    public static class RectanglesHelper
    {
        public static bool
            HaveRectangleIntersectWithAnother(Rectangle baseRectangle, IEnumerable<Rectangle> rectangles) =>
            rectangles.Any(rectangle => rectangle.IntersectsWith(baseRectangle));

        public static IEnumerable<Rectangle> GetAllPossibleRectangles(Point corner, Size size)
        {
            yield return new Rectangle(corner.X, corner.Y - size.Height, size.Width, size.Height);
            yield return new Rectangle(corner, size);
            yield return new Rectangle(corner.X - size.Width, corner.Y, size.Width, size.Height);
            yield return new Rectangle(corner - size, size);
        }

        public static IEnumerable<Point> GetCorners(Rectangle rectangle)
        {
            yield return rectangle.Location;
            yield return new Point(rectangle.X + rectangle.Width, rectangle.Y);
            yield return new Point(rectangle.X, rectangle.Y + rectangle.Height);
            yield return rectangle.Location + rectangle.Size;
        }
    }
}