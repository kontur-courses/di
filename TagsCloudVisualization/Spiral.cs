using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class Spiral
    {
        private readonly double parameter;
        private readonly double stepInRadians;
        private double phi;

        public Spiral(double parameter, int stepInDegrees)
        {
            this.parameter = parameter;
            stepInRadians = stepInDegrees * Math.PI / 180;
        }

        public Point GetNextPoint()
        {
            var r = parameter * phi;
            var point =  GeometryUtils.ConvertPolarToIntegerCartesian(r, phi);
            phi += stepInRadians;
            return point;
        }
    }
}