using System;
using System.Drawing;

namespace TagsCloudGenerator
{
    public static class RectangleExtensions
    {
        public static double GetDistanceToPoint(this Rectangle rec, Point point)
        {
            return Math.Sqrt(Math.Pow(rec.X - point.X, 2) + 
                             Math.Pow(rec.Y - point.Y, 2));
        }

        public static RectangleF ConvertToRectangleF(this Rectangle rec) => new RectangleF(rec.Location, rec.Size);

        public static Point GetCenter(this Rectangle rect)
        {
            return new Point(
                rect.X + rect.Width / 2,
                rect.Y + rect.Height / 2);
        }

        public static Rectangle ShiftLocation(this Rectangle rect, Point shift)
        {
            return new Rectangle(new Point(rect.X + shift.X, rect.Y + shift.Y), rect.Size);
        }
    }
}