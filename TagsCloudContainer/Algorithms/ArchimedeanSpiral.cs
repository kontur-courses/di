using System;
using System.Drawing;

namespace TagsCloudContainer.Algorithms
{
    public class ArchimedeanSpiral : ISpiral
    {
        private readonly Point center;
        private double spiralAngle;

        public ArchimedeanSpiral(Point center)
        {
            this.center = center;
        }

        public Point GetNextPoint()
        {
            var x = center.X + (int)(spiralAngle * Math.Cos(spiralAngle));
            var y = center.Y + (int)(spiralAngle * Math.Sin(spiralAngle));

            spiralAngle++;

            return new Point(x,y);
        }

        public double GetCurrentSpiralAngle() => spiralAngle;
    }
}
