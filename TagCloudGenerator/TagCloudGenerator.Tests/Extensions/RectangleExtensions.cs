using System;
using System.Drawing;

namespace TagCloudGenerator.Tests.Extensions
{
    public static class RectangleExtensions
    {
        public static Point GetRectangleCenter(this Rectangle rectangle)
        {
            var xCenter = (int)Math.Round(rectangle.X + rectangle.Width / 2.0, MidpointRounding.AwayFromZero);
            var yCenter = (int)Math.Round(rectangle.Y + rectangle.Height / 2.0, MidpointRounding.AwayFromZero);

            return new Point(xCenter, yCenter);
        }

        private static double GetDiagonal(this Rectangle rectangle) =>
            Math.Sqrt(rectangle.Width * rectangle.Width + rectangle.Height * rectangle.Height);

        private static double GetCircumscribedCircleRadius(this Rectangle rectangle) => rectangle.GetDiagonal() / 2;

        public static bool CheckIfPointIsCenterOfRectangle(this Rectangle rectangle, Point point, double precision)
        {
            if (!rectangle.Contains(point))
                return false;

            var topRight = new Point(rectangle.Right, rectangle.Top);
            var bottomRight = new Point(rectangle.Right, rectangle.Bottom);
            var circumcircleRadius = rectangle.GetCircumscribedCircleRadius();

            return rectangle.Location.GetDistanceToPoint(point).IsApproximatelyEqual(circumcircleRadius, precision) &&
                   topRight.GetDistanceToPoint(point).IsApproximatelyEqual(circumcircleRadius, precision) &&
                   bottomRight.GetDistanceToPoint(point).IsApproximatelyEqual(circumcircleRadius, precision);
        }
    }
}