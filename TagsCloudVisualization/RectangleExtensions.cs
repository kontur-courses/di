using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public static class RectangleExtensions
    {
        public static Rectangle Move(this Rectangle rectangle, Point offset)
        {
            rectangle.X += offset.X;
            rectangle.Y += offset.Y;

            return rectangle;
        }

        public static IEnumerable<Point> Corners(this Rectangle rectangle)
        {
            yield return new Point(rectangle.Left, rectangle.Top);
            yield return new Point(rectangle.Left, rectangle.Bottom);
            yield return new Point(rectangle.Right, rectangle.Top);
            yield return new Point(rectangle.Right, rectangle.Bottom);
        }
    }
}
