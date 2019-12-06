using System;
using System.Drawing;

namespace TagCloudGenerator.Tests.Extensions
{
    public static class PointExtensions
    {
        public static Rectangle GetRectangleWithCenterInThePoint(this Point centerPoint, Size rectangleSize)
        {
            var xTopLeft = (int)Math.Round(centerPoint.X - rectangleSize.Width / 2.0, MidpointRounding.AwayFromZero);
            var yTopLeft = (int)Math.Round(centerPoint.Y - rectangleSize.Height / 2.0, MidpointRounding.AwayFromZero);

            return new Rectangle(new Point(xTopLeft, yTopLeft), rectangleSize);
        }

        public static double GetDistanceToPoint(this Point point, Point otherPoint)
        {
            double xDelta = point.X - otherPoint.X;
            double yDelta = point.Y - otherPoint.Y;

            return Math.Sqrt(Math.Pow(xDelta, 2) + Math.Pow(yDelta, 2));
        }
    }
}