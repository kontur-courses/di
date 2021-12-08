using System;
using System.Drawing;

namespace TagsCloudVisualization.PointGenerators
{
    public class ArchimedeanSpiralPointGenerator : IPointGenerator
    {
        private readonly double coefficient;
        private readonly Point start;
        private readonly double step;
        private double angle;

        public ArchimedeanSpiralPointGenerator(Point start, double coefficient = 1, double angleDelta = Math.PI / 360)
        {
            this.start = start;
            this.coefficient = coefficient;
            step = angleDelta;
        }

        public Point GetNextPoint()
        {
            var x = Convert.ToInt32(coefficient * Math.Cos(angle) * angle + start.X);
            var y = Convert.ToInt32(coefficient * Math.Sin(angle) * angle + start.Y);
            angle += step;
            return new Point(x, y);
        }
    }
}