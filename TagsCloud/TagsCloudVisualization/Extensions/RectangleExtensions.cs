using System.Drawing;

namespace TagsCloudVisualization.Extensions
{
    internal static class RectangleExtensions
    {
        public static Point GetCenter(this Rectangle rect)
            => new(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);

        public static Rectangle Shift(this Rectangle rect, Size size) => new(rect.Location + size, rect.Size);
    }
}