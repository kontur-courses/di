using System;
using System.Drawing;


namespace TagCloud
{
    public class ArchimedianSprial : IAlgorithm
    {
        public double Alpha { get; private set; } = Math.PI / 40;
        public double Step { get; } = Math.PI / 40;
        public double DensityCoefficient { get; } = 0.2;
        public Point Center { get; }
        private double RadiusVector { get; set; } = 0.0;

        public ArchimedianSprial()
        {
            Center = Settings.Center;
        }

        public Point GetNextCoordinate()
        {
         
            Alpha += Step;
            RadiusVector = Alpha * DensityCoefficient;
            var x = (int)(RadiusVector * Math.Cos(Alpha)) + Center.X;
            var y = (int)(RadiusVector * Math.Sin(Alpha)) + Center.Y;
            return new Point(x, y);
        }
    }
}
