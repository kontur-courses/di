using System;
using TagsCloudContainer.CircularCloudLayouters;
using Point = System.Drawing.Point;

namespace TagsCloudContainer.Extensions
{
    public static class PointExtensions
    {
        public static Point Shift(this Point point, int x, int y)
            => new Point(point.X + x, point.Y + y);

        public static double DistanceTo(this Point point, Point secondPoint)
        {
            var differenceX = secondPoint.X - point.X;
            var differenceY = secondPoint.Y - point.Y;
            return Math.Sqrt(differenceY * differenceY + differenceX * differenceX);
        }

        public static Point Sum(this Point point, Vector vector)
        {
            return point.Shift(vector.X, vector.Y);
        }
    }
}