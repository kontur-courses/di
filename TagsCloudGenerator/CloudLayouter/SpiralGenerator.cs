using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudGenerator.CloudLayouter
{
    public static class SpiralGenerator
    {
        public static IEnumerable<Point> GetSpiral(Point center, double spiralStep, double angleStep = 0.5)
        {
            var angle = 0.0;
            var k = spiralStep / (Math.PI * 2);

            while (true)
            {
                var x = k * angle * Math.Cos(angle) + center.X;
                var y = k * angle * Math.Sin(angle) + center.Y;

                yield return new Point((int)x, (int)y);

                angle += angle < 500 ? angleStep : angleStep / 2;
            }
        }
    }
}