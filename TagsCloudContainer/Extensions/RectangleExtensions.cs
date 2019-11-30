using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer.Extensions
{
    public static class RectangleExtensions
    {
        public static Point GetCenteredLocation(this Rectangle rectangle, Point point)
        {
            return new Point(point.X - rectangle.Width / 2, point.Y - rectangle.Height / 2);
        }

        public static bool IntersectsWithAny(this Rectangle rectangle, IEnumerable<Rectangle> rectangles)
        {
            return rectangles.Any(rect => rect.IntersectsWith(rectangle));
        }

        public static Rectangle GetCentered(this Rectangle rectangle, Point point)
        {
            var centeredLocation = GetCenteredLocation(rectangle, point);
            return new Rectangle(centeredLocation, rectangle.Size);
        }

        public static Point GetCenter(this Rectangle rectangle)
        {
            return new Point(rectangle.X + rectangle.Width / 2, rectangle.Y + rectangle.Height / 2);
        }
    }
}