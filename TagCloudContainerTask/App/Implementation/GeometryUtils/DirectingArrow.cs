using System;
using System.Drawing;

namespace App.Implementation.GeometryUtils
{
    public class DirectingArrow
    {
        private readonly Point startPoint;
        private double angleRadian;

        private int radiusVectorLength;

        public DirectingArrow(Point startPoint)
        {
            this.startPoint = startPoint;
            angleRadian = 0;
            radiusVectorLength = 0;
        }

        public Point Rotate()
        {
            var rotationAngle = Math.PI / (6 + radiusVectorLength);
            angleRadian += rotationAngle;

            if (angleRadian >= 2 * Math.PI)
            {
                angleRadian -= 2 * Math.PI;
                radiusVectorLength += 1;
            }

            var x = (int)(startPoint.X + radiusVectorLength * Math.Cos(angleRadian));
            var y = (int)(startPoint.Y + radiusVectorLength * Math.Sin(angleRadian));

            return new Point(x, y);
        }
    }
}