using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TagsCloudVisualization.Curves
{
    public class Spiral : ICurve
    {
        private readonly Point center;
        private readonly double deltaAngle;
        private double angle;
        private readonly double spiralPitch;

        public Spiral(Point center, double deltaAngle = Math.PI / 180, double spiralPitch = 2)
        {
            this.center = center;
            this.deltaAngle = deltaAngle;
            this.spiralPitch = spiralPitch;
        }

        private IEnumerable<Point> PointGenerator()
        {
            while (true)
            {
                var p = spiralPitch * angle;
                var x = (int)(p * Math.Cos(angle)) + center.X;
                var y = (int)(p * Math.Sin(angle)) + center.Y;
                angle += deltaAngle;
                yield return new Point(x, y);
            }
        }

        public IEnumerator<Point> GetEnumerator()
            => PointGenerator().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}
