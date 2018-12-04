using System.Drawing;

namespace TagsCloudVisualization
{
    public static class VisualizatorExtensions
    {
        public static RectangleF ToRectangleF(this Rectangle rectangle)
        {
            var location = ToPointF(rectangle.Center);
            var size = ToSizeF(rectangle.Size);

            return new RectangleF(location, size);
        }

        public static PointF ToPointF(this Point point)
            => new PointF((float)point.X, (float)point.Y);

        public static SizeF ToSizeF(this Size size)
            => new SizeF((float)size.Width, (float)size.Height);
    }
}
