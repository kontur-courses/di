using System;
using System.Drawing;

namespace TagCloud.Tests.Extensions
{
    public static class PointExtensions
    {
        public static double GetDistanceTo(this PointF point, PointF anotherPoint)
        {
            var dx = point.X - anotherPoint.X;
            var dy = point.Y - anotherPoint.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public static PointF Add(this PointF point, PointF anotherPoint)
        {
            var x = point.X + anotherPoint.X;
            var y = point.Y + anotherPoint.Y;
            return new PointF(x, y);
        }
    }
}