using System.Drawing;

namespace TagCloudContainer.PointAlgorithm
{
    public class ArithmeticSpiral: IPointer
    {
        private int x, y;
        private double angle;
        private int constantEllipsoid, density;

        public ArithmeticSpiral(Point start, int constantEllipsoid = 1, int density = 1)
        {
            x = start.X;
            y = start.Y;
            this.constantEllipsoid = constantEllipsoid;
            this.density = density;
        }
        public Point GetPoint()
        {
            var nextPoint = new Point((int)(x + Math.Cos(angle) * angle * constantEllipsoid),
                (int)(y + Math.Sin(angle) * angle));
            angle += Math.PI / (360 * density);
            return nextPoint;
        }
    }
}