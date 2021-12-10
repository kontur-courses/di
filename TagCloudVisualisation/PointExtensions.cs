using System;
using System.Drawing;

namespace TagCloudVisualisation
{
    public static class PointExtensions
    {
        public static double GetDistanceTo(this Point p1, Point p2)
        {
            var dX = p1.X - p2.X;
            var dY = p1.Y - p2.Y;
            return Math.Sqrt(dX * dX + dY * dY);
        }

        public static Point Displace(this Point p1, int dX, int dY)
        {
            return new Point(p1.X + dX, p1.Y + dY);
        }

        public static Rectangle GetRectangleWithCenterInPoint(this Point center, Size rectangleSize)
        {
            var locationX = center.X - rectangleSize.Width / 2;
            var locationY = center.Y - rectangleSize.Height / 2;
            return new Rectangle(new Point(locationX, locationY), rectangleSize);
        }
    }
}
