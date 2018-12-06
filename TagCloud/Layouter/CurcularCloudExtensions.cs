using System;
using System.Collections.Generic;
using System.Linq;

namespace TagCloud.Layouter
{
    public static class CurcularCloudExtensions
    {
        public static double GetWidth(this IEnumerable<Rectangle> cloud)
        {
            var minLeftX = cloud.Select(r => r.Left).Min();
            var maxRightX = cloud.Select(r => r.Right).Max();

            return Math.Max(Math.Abs(minLeftX), Math.Abs(maxRightX));
        }

        public static double GetHeight(this IEnumerable<Rectangle> cloud)
        {
            var minBottomY = cloud.Select(r => r.Bottom).Min();
            var maxTopY = cloud.Select(r => r.Top).Max();

            return Math.Max(Math.Abs(minBottomY), Math.Abs(maxTopY));
        }
    }
}