using System;
using System.Collections.Generic;

namespace TagCloudVisualization
{
    public class RoundSpiralGenerator : AbstractSpiralGenerator
    {
        private const double AngleDelta = Math.PI / 64;

        /// <summary>
        ///     r = K*ф
        /// </summary>
        private const double K = 2;

        private protected override IEnumerator<Point> GetEnumerator()
        {
            var angle = 0d;
            yield return Center + PolarCoordinatesToCartesian(0d, 0d);
            while (true)
            {
                angle += AngleDelta;
                yield return Center + PolarCoordinatesToCartesian(K * angle, angle);
            }
        }

        private static Point PolarCoordinatesToCartesian(double distance, double angle)
        {
            var x = distance * Math.Cos(angle);
            var y = distance * Math.Sin(angle);
            return new Point((int) x, (int) y);
        }
    }
}
