using System;
using System.Drawing;

namespace TagCloud.CloudLayouter.FigurePaths
{
    public class Spiral : IFigurePath
    {
        private readonly double turnsDistance;
        private readonly double deltaAngle;
        private double angle;

        /// <summary>
        /// Create new Spiral
        /// </summary>
        /// <param name="turnsDistance">distance between different turns</param>
        /// <param name="deltaAngle">Angle between next and previous point in degrees</param>
        public Spiral(double turnsDistance = 1, double deltaAngle = 2)
        {
            this.turnsDistance = turnsDistance / 2 / Math.PI;
            this.deltaAngle = deltaAngle * Math.PI / 180;
            angle = -this.deltaAngle;
        }

        public Point GetNextPoint()
        {
            angle += deltaAngle;
            var distance = turnsDistance * angle;
            return getPointFromPolarCoordinates(distance, angle);
        }

        private Point getPointFromPolarCoordinates(double r, double angle)
        {
            return new Point((int)(r * Math.Cos(angle)), (int)(r * Math.Sin(angle)));
        }
    }
}
