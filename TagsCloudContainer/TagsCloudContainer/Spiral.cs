﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;

namespace TagsCloudVisualization
{
    public class Spiral
    {
        private readonly Point center;

        public Spiral(Point center)
        {
            this.center = center;
        }

        public IEnumerable<Point> GetPoints()
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