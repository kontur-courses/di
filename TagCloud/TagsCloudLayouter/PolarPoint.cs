using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudLayouter
{
    public readonly struct PolarPoint
    {
        public readonly double Angle;
        public readonly double Radius;

        public PolarPoint(double radius, double angle)
        {
            Angle = angle;
            Radius = radius;
        }

        public static explicit operator Point(PolarPoint polarPoint)
        {
            var x = (int)Math.Round(polarPoint.Radius * Math.Cos(polarPoint.Angle));
            var y = (int)Math.Round(polarPoint.Radius * Math.Sin(polarPoint.Angle));
            return new Point(x, y);
        }
    }
}