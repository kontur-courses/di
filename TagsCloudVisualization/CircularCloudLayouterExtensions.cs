using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    public static class CircularCloudLayouterExtensions
    {
        public static Rectangle ShiftCoordinatesToCenterRectangle(this Rectangle rectangle)
        {
            var location = rectangle.Location;
            location.Offset(new Point(- rectangle.Width / 2, - rectangle.Height / 2));

            return new Rectangle(location, rectangle.Size);
        }

        public static double GetDistanceToPoint(this Rectangle rectangle, Point point)
        {
            return Math.Sqrt((GetCenter(rectangle).X - point.X) *
                             (GetCenter(rectangle).X - point.X) +
                             (GetCenter(rectangle).Y - point.Y) *
                             (GetCenter(rectangle).Y - point.Y));
        }

        public static Point GetCenter(this Rectangle rectangle)
        {
            var location = rectangle.Location;
            location.Offset(new Point(rectangle.Width / 2, rectangle.Height / 2));

            return location;
        }
    }
}
