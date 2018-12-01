using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public static class SpiralPath
    {
        private static Point ArchimedeanSpiral(double spiralStepCoefficient, double radius)
        {
            var angleRadians = radius * (2 * Math.PI) / spiralStepCoefficient;
            var x = (int) (radius * Math.Cos(angleRadians));
            var y = (int) (radius * Math.Sin(angleRadians));

            return new Point(x, y);
        }

        public static IEnumerable<Point> GetSpiralPoints(double spiralStepCoefficient, double radiusStep)
        {
            for (var radius = 0d; radius < double.MaxValue; radius += radiusStep)
            {
                yield return ArchimedeanSpiral(spiralStepCoefficient, radius);
            }
        }
    }
}
