using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud
{
    public class Spiral
    {
        public Point Center { get; }

        public Spiral(Point center)
        {
            Center = center;
        }

        public IEnumerable<Point> GetPoints()
        {
            for (var i = 0; ; i++)
            {
                var x = (int)(i * Math.Cos(i * 0.5)) + Center.X;
                var y = (int)(i * Math.Sin(i * -0.5)) + Center.Y;
                yield return new Point(x, y);
            }
        }
    }
}
