using System;
using System.Drawing;

namespace TagsCloudContainer.Layouter
{
    public class SpiralPointsGenerator
    {
        private double spiralCoeff = 1 / (2 * Math.PI);
        private double angleStep = 3.14 / 16;
        private double angle = 0;
        
        public Point GetNextSpiralPoint()
        {
            int X = (int) Math.Floor(spiralCoeff * angle * Math.Cos(angle));
            int Y = (int) Math.Floor(spiralCoeff * angle * Math.Sin(angle));
            var point = new Point(X, Y);
            angle += angleStep;
            return point;
        }
    }
}