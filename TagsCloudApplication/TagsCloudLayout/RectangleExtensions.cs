using System.Drawing;

namespace TagsCloudLayout
{
    public static class RectangleExtensions
    {
        public static PointF GetCenter(this Rectangle rect)
        {
            return new PointF(
                rect.Left + (float)rect.Width / 2, 
                rect.Top + (float)rect.Height / 2);
        }

        public static Rectangle OffsetByMassCenter(this Rectangle rect)
        {
            rect.Offset(-rect.Width / 2, -rect.Height / 2);
            return rect;
        }

        public static Rectangle GetShiftedToNewCenter(this Rectangle rect, 
            Point oldCenter, Point newCenter)
        {
            rect.Offset(new Point(newCenter.X - oldCenter.X, newCenter.Y - oldCenter.Y));
            return rect;
        }
    }
}
