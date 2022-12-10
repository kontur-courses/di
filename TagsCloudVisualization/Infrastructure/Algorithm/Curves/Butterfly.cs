using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization.Infrastructure.Algorithm.Curves
{
    public class Butterfly : ICurve
    {
        public Butterfly(Point center)
        {
            Center = center;
        }

        public IEnumerator<Point> GetEnumerator()
        {
            for (var i = 0.0;; i += 0.1) yield return GetPoint(i);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Point Center { get; }


        private Point GetPoint(double t)
        {
            var x = Center.X + Math.Sin(t) *
                (Math.Pow(Math.E, Math.Cos(t)) - 2 * Math.Cos(4 * t) + Math.Pow(Math.Sin(112 * t), 5)) *
                ((int)(t / (12 * Math.PI)) != 0 ? t / Math.PI : 1);
            var y = Center.Y + Math.Cos(t) *
                (Math.Pow(Math.E, Math.Cos(t)) - 2 * Math.Cos(4 * t) + Math.Pow(Math.Sin(112 * t), 5)) *
                ((int)(t / (12 * Math.PI)) != 0 ? t / Math.PI : 1);
            return new Point((int)x, (int)y);
        }
    }
}