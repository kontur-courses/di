using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud.Layouter
{
    public class Spiral : ISpiral
    {
        public Point Center { get; private set; }

        public Spiral()
        {
            Center = new Point();
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

        public void SetCenter(Point center) => Center = center;
    }
}
