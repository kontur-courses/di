using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization.Infrastructure.Algorithm.Curves
{
    public class Spiral : ICurve
    {
        private readonly float angleIncrement;
        private readonly float distanceBetweenLoops;

        public Spiral(Point center, float distanceBetweenLoops = 1, float angleIncrement = 0.02f)
        {
            if (distanceBetweenLoops == 0)
                throw new ArgumentException(
                    "distanceBetweenLoops cannot be zero");

            if (angleIncrement == 0)
                throw new ArgumentException(
                    "angleIncrement cannot be zero");

            this.angleIncrement = angleIncrement;
            this.distanceBetweenLoops = distanceBetweenLoops / (float)(2 * Math.PI);
            Center = center;
        }

        public Point Center { get; }

        public IEnumerator<Point> GetEnumerator()
        {
            for (var angle = 0f;; angle += angleIncrement) yield return GetArchimedeanSpiralPoint(angle);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private Point GetArchimedeanSpiralPoint(float angle)
        {
            var x = (int)Math.Round(Center.X + distanceBetweenLoops * angle * Math.Cos(angle));
            var y = (int)Math.Round(Center.Y - distanceBetweenLoops * angle * Math.Sin(angle));
            return new Point(x, y);
        }
    }
}