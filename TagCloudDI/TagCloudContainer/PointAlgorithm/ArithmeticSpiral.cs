using System.Drawing;
using TagCloudContainer.Interfaces;

namespace TagCloudContainer.PointAlgorithm
{
    public class ArithmeticSpiral : IPointProvider
    {
        private double angle;
        public Point GetNextPoint()
        {
            var nextPoint = new Point((int)(Math.Cos(angle) * angle),
                (int)(Math.Sin(angle) * angle));

            angle += Math.PI / (360);

            return nextPoint;
        }

        public void Reset()
        {
            angle = 0;
        }
    }
}