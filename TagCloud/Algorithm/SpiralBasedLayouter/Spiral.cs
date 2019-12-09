using System;
using System.Drawing;

namespace TagCloud.Algorithm.SpiralBasedLayouter
{
    public class Spiral
    {
        private readonly double parameter;
        private readonly Point center;
        private readonly double stepInRadians;
        private double phi;

        public Spiral(double parameter, int stepInDegrees, Point center)
        {
            this.parameter = parameter;
            this.center = center;
            stepInRadians = stepInDegrees * Math.PI / 180;
        }

        public Point GetNextPoint()
        {
            var r = parameter * phi;
            var point =  GeometryUtils.ConvertPolarToIntegerCartesian(r, phi);
            phi += stepInRadians;
            return new Point(point.X + center.X, point.Y + center.Y);
        }
    }
}