using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Algorithm.Curves
{
    public class Spiral : ICurve
    {
        private readonly float distanceBetweenLoops;
        private readonly float angleIncrement;

        public Point Center { get; }

        public Spiral(float distanceBetweenLoops, Point center, float angleIncrement = 0.02f)
        {
            if (distanceBetweenLoops == 0)
            {
                throw new ArgumentException(
                    $"distanceBetweenLoops cannot be zero");
            }

            if (angleIncrement == 0)
            {
                throw new ArgumentException(
                    $"angleIncrement cannot be zero");
            }

            this.angleIncrement = angleIncrement;
            this.distanceBetweenLoops = distanceBetweenLoops / (float)(2 * Math.PI);
            this.Center = center;
        }

        public IEnumerator<Point> GetEnumerator()
        {
            for (var angle = 0f; ; angle += angleIncrement)
            {
                yield return GetArchimedeanSpiralPoint(angle);
            }
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
