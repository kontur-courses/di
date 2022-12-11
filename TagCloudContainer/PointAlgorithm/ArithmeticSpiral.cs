using System.Drawing;

namespace TagCloudContainer.PointAlgorithm
{
    public class ArithmeticSpiral : IPointer
    {
        private double angle;
        public IPointConfig Config { get; set; }
        public Point GetNextPoint()
        {
            var nextPoint = new Point((int)(Math.Cos(angle) * angle * Config.EllipsoidMultiplier),
                (int)(Math.Sin(angle) * angle));
            angle += Math.PI / (360 * Config.DensityMultiplier);
            return nextPoint;
        }

        public void Reset()
        {
            angle = 0;
        }
    }
}