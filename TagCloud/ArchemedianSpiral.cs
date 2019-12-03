using System;
using System.Drawing;

namespace TagCloud
{
    public class ArchimedianSprial : IAlgorithm
    {
        public double Alpha { get; private set; } = Math.PI / 40;
        public double Step { get; } = Math.PI / 40;
        public double DensityCoefficient { get; } = 0.2;
        private double RadiusVector { get; set; }
        public Point GetNextCoordinate()
        {
            Alpha += Step;
            RadiusVector = Alpha * DensityCoefficient;
            var x = (int) (RadiusVector * Math.Cos(Alpha));
            var y = (int) (RadiusVector * Math.Sin(Alpha));
            return new Point(x, y);
        }
    }
}