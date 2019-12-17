using System;
using System.Collections.Generic;
using System.Drawing;

namespace CloudLayouter.Spiral
{
    public class CircularSpiral : ISpiral
    {
        public IEnumerable<Point> GetPoints(Point center)
        {
            var radius = 0.0; 
            var angle = 0.0;
            while (true)
            {
                var point = ConvertingBetweenPolarToCartesianCoordinates(radius, angle);
                point.Offset(center);
                yield return point;

                radius = angle < Math.PI * 2 ? radius : radius + 1; 
                angle = angle < Math.PI * 2 ?  angle + 0.1 : angle - Math.PI * 2  ;
            }
        }

        public static Point ConvertingBetweenPolarToCartesianCoordinates(double radius, double angle)
        {
            var x = (int) Math.Round(radius * Math.Cos(angle));
            var y = (int) Math.Round(radius * Math.Sin(angle));
            return new Point(x,y);
        }
    }
}