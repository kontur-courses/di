using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudGenerator.Infrastructure
{
    public static class RectangleExtensions
    {
        public static Point GetCenter(this Rectangle rect)
        {
            return new Point(
                rect.X + rect.Width / 2,
                rect.Y + rect.Height / 2);
        }

        public static double DistanceToPoint(this Rectangle rect, Point point)
        {
            return rect.GetCenter().DistanceTo(point);
        }

        public static Rectangle ShiftLocation(this Rectangle rect, Point shift)
        {
            return new Rectangle(new Point(rect.X + shift.X, rect.Y + shift.Y), rect.Size);
        }

        public static Point GetTopRightCorner(this Rectangle rect)
        {
            return new Point(rect.Right, rect.Y);
        }

        public static Point GetBottomLeftCorner(this Rectangle rect)
        {
            return new Point(rect.X, rect.Bottom);
        }

        public static Point GetBottomRightCorner(this Rectangle rect)
        {
            return new Point(rect.Right, rect.Bottom);
        }

        public static IEnumerable<Point> GetCornersClockwiseFromTopLeft(this Rectangle rect)
        {
            yield return rect.Location;
            yield return rect.GetTopRightCorner();
            yield return rect.GetBottomRightCorner();
            yield return rect.GetBottomLeftCorner();
        }
    }
}