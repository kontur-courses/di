using System;
using System.Collections;
using System.Collections.Generic;

namespace TagsCloudVisualization
{
    internal class RoundSpiralGenerator : IEnumerable<Point>
    {
        private const double AngleDelta = Math.PI / 64;
        private readonly Point center;
        private readonly double k;

        /// <param name="center">Центр спирали</param>
        /// <param name="k">Коэффициент размера в уравнении R = k * ф</param>
        public RoundSpiralGenerator(Point center, double k)
        {
            this.center = center;
            this.k = k;
        }

        public IEnumerator<Point> GetEnumerator()
        {
            var angle = 0d;
            yield return center + PolarCoordinatesToCartesian(0d, 0d);
            while (true)
            {
                angle += AngleDelta;
                yield return center + PolarCoordinatesToCartesian(k * angle, angle);
            }

            // ReSharper disable once IteratorNeverReturns
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private static Point PolarCoordinatesToCartesian(double distance, double angle)
        {
            var x = distance * Math.Cos(angle);
            var y = distance * Math.Sin(angle);
            return new Point((int) x, (int) y);
        }
    }
}
