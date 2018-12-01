using System.Collections.Generic;
using System.Drawing;

namespace TagCloud
{
    public static class RectangleExtensions
    {
        public static Point GetCenter(this Rectangle rectangle) =>
            new Point(rectangle.Left + rectangle.Width / 2, rectangle.Top + rectangle.Height / 2);

        public static Rectangle MoveToHavePointInCenter(this Rectangle rectangle, Point center)
        {
            var newLocation = center - new Size(rectangle.Width / 2, rectangle.Height / 2);
            return new Rectangle(newLocation, rectangle.Size);
        }

        public static IEnumerable<Point> GetCorners(this Rectangle rectangle)
        {
            yield return new Point(rectangle.Left, rectangle.Top);
            yield return new Point(rectangle.Left, rectangle.Bottom);
            yield return new Point(rectangle.Right, rectangle.Top);
            yield return new Point(rectangle.Right, rectangle.Bottom);
        }
    }
}