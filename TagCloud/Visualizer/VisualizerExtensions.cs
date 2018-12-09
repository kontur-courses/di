using System.Drawing;
using Point = TagCloud.Layouter.Point;
using Rectangle = TagCloud.Layouter.Rectangle;
using Size = TagCloud.Layouter.Size;

namespace TagCloud.Visualizer
{
    public static class VisualizerExtensions
    {
        public static RectangleF ToRectangleF(this Rectangle rectangle)
        {
            var location = ToPointF(rectangle.Center);
            var size = ToSizeF(rectangle.Size);

            return new RectangleF(location, size);
        }

        public static PointF ToPointF(this Point point)
        {
            return new PointF((float) point.X, (float) point.Y);
        }

        public static SizeF ToSizeF(this Size size)
        {
            return new SizeF((float) size.Width, (float) size.Height);
        }

        public static Size ToSize(this SizeF size)
        {
            return new Size((int) size.Width, (int) size.Height);
        }

        public static System.Drawing.Size ToSize(this Size size)
        {
            return new System.Drawing.Size((int) size.Width, (int) size.Height);
        }
    }
}