using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization.Infrastructure
{
    public class Spiral
    {
        private double angle;
        private readonly double step;
        private readonly double angleDelta;
        private readonly Point center;

        public Spiral(Point center, double step = 0.5, double angleDelta = Math.PI / 36)
        {
            this.step = step;
            this.angleDelta = angleDelta;
            this.center = center;
        }

        public IEnumerable<Point> GetPointsLazy()
        {
            while (true)
            {
                var x = Convert.ToInt32(step / (Math.PI * 2) * angle * Math.Cos(angle));
                var y = Convert.ToInt32(step / (Math.PI * 2) * angle * Math.Sin(angle));
                angle += angleDelta;
                x += center.X;
                y += center.Y;
                yield return new Point(x, y);
            }
        }
    }
}