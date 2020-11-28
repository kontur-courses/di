using System.Drawing;

namespace WordCloudGenerator
{
    public static class RectangleExtensions
    {
        public static RectangleF SetCenter(this RectangleF rectangle, Point centerPoint)
        {
            rectangle.X = centerPoint.X - rectangle.Width / 2;
            rectangle.Y = centerPoint.Y - rectangle.Height / 2;
            return rectangle;
        }

        public static RectangleF Shift(this RectangleF rectangle, PointF shift)
        {
            rectangle.Offset(shift);
            return rectangle;
        }
    }
}