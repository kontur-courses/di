using System;
using System.Drawing;

namespace TagsCloud.Tests
{
    public static class PointExtensions
    {
        public static double GetDistance(this Point from, Point to) =>
            Math.Sqrt((to.X - from.X) * (to.X - from.X) + (to.Y - from.Y) * (to.Y - from.Y));
    }
}