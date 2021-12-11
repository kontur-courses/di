using System;
using System.Drawing;

namespace App.Implementation.GeometryUtils
{
    public static class PointExtension
    {
        public static Point MovePoint(this Point point, int x, int y)
        {
            return new Point(point.X + x, point.Y + y);
        }

        public static Point MovePointToSizeCenter(this Point point, Size size, bool isImgSize)
        {
            var direction = isImgSize ? 1 : -1;
            return point.MovePoint(direction * size.Width / 2, direction * size.Height / 2);
        }

        public static double GetDistanceTo(this Point from, Point to)
        {
            return Math.Sqrt((to.X - from.X) * (to.X - from.X)
                             + (to.Y - from.Y) * (to.Y - from.Y));
        }
    }
}