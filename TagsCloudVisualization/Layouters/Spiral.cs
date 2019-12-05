using System;
using System.Drawing;

namespace TagsCloudVisualization.Layouters
{
    public class Spiral
    {
        public double Radius { get; private set; }
        public double Angle { get; private set; }
        private double densityCoefficient;
        private readonly double densityShift;
        public readonly Point Center;
        private readonly double angleShift;
        private readonly double radiusShift;
        
        public Spiral(Point center, double densityCoefficient = 1, double densityShift = 0.01, double angleShift = 0.2,
            double radiusShift = 0.5)
        {
            Center = center;
            this.angleShift = angleShift;
            this.radiusShift = radiusShift;
            this.densityCoefficient = densityCoefficient;
            this.densityShift = densityShift;
        }
        
        public Point GetNextPoint()
        {
            Radius += radiusShift / densityCoefficient;
            Angle += angleShift;
            densityCoefficient += densityShift;

            return new Point((int) (Center.X + Radius * Math.Cos(Angle)),
                (int) (Center.Y + Radius * Math.Sin(Angle)));
        }
    }
}