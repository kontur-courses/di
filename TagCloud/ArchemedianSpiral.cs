using System;
using System.Drawing;

namespace TagCloud
{
    public class ArchimedianSprial : IAlgorithm
    {
        private  double alpha;
        private const double Step = Math.PI / 40;
        private const double DensityCoefficient = 0.2;

        public Point GetNextCoordinate()
        {
            alpha += Step;
            var radiusVector = alpha * DensityCoefficient;
            var x = (int) (radiusVector * Math.Cos(alpha));
            var y = (int) (radiusVector * Math.Sin(alpha));
            return new Point(x, y);
        }

        public void Update()
        {
            alpha = 0;
        }
    }
}