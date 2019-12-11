using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagCloud
{
    public class ArchimedianSprial : IAlgorithm
    {
        private const double Step = Math.PI / 40;
        private const double DensityCoefficient = 0.2;
        private double alpha;

        public IEnumerable<Point> GetCoordinates()
        {
            alpha = 0;
            while (true)
            {
                alpha += Step;
                var radiusVector = alpha * DensityCoefficient;
                var x = (int) (radiusVector * Math.Cos(alpha));
                var y = (int) (radiusVector * Math.Sin(alpha));
                yield return new Point(x, y);
            }
        }
    }
}