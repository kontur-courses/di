using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TagsCloudContainer.Layouter
{
    public static class PointExtensions
    {
        public static int SquaredDistanceTo(this Point point, Point otherPoint)
        {
            var xDiff = point.X - otherPoint.X;
            var yDiff = point.Y - otherPoint.Y;
            return xDiff * xDiff + yDiff * yDiff;
        }

        public static double DistanceTo(this Point point, Point otherPoint)
        {
            return Math.Sqrt(point.SquaredDistanceTo(otherPoint));
        }
    }
}
