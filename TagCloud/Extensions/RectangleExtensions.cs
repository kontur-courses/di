using System.Drawing;

namespace TagCloud.Extensions
{
    public static class RectangleExtensions
    {
        public static Point GetCenter(this Rectangle rectangle)
        {
            return new Point(rectangle.Left + rectangle.Width / 2, rectangle.Top + rectangle.Height / 2);
        }
    }
}