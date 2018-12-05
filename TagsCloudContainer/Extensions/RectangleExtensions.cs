using System.Drawing;

namespace TagsCloudContainer.Extensions
{
    public static class RectangleExtensions
    {
        public static Point[] Vertices(this Rectangle rectangle)
        {
            return new[]
            {
                rectangle.Location,
                new Point(rectangle.Location.X + rectangle.Width, rectangle.Location.Y),
                rectangle.Location + rectangle.Size,
                new Point(rectangle.Location.X, rectangle.Location.Y + rectangle.Height)
            };
        }
    }
}
