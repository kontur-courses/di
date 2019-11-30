using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class Spiral
    {
        public double Radius { get; private set; }
        public double Angle { get; private set; }
        private double densityCoefficient;
        private readonly double densityShift;
        private readonly Point center;
        private readonly double angleShift;
        private readonly double radiusShift;
        
        public Spiral(Point center, double densityCoefficient = 1, double densityShift = 0.01, double angleShift = 0.2,
            double radiusShift = 0.5)
        {
            this.center = center;
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

            return new Point((int) (center.X + Radius * Math.Cos(Angle)),
                (int) (center.Y + Radius * Math.Sin(Angle)));
        }
    }
}