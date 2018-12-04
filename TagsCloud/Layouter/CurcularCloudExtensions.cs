using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization
{
    public static class CurcularCloudExtensions
    {
        public static double GetWidth(this IEnumerable<Rectangle> cloud)
        {
            var minLeftX = cloud.Select(r => r.LeftXCoord).Min();
            var maxRightX = cloud.Select(r => r.RightXCoord).Max();

            return Math.Max(Math.Abs(minLeftX), Math.Abs(maxRightX));
        }

        public static double GetHeight(this IEnumerable<Rectangle> cloud)
        {
            var minBottomY = cloud.Select(r => r.BottomYCoord).Min();
            var maxTopY = cloud.Select(r => r.TopYCoord).Max();

            return Math.Max(Math.Abs(minBottomY), Math.Abs(maxTopY));
        }
    }
}
