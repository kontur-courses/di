using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloud.Layouter.Base;

namespace TagsCloud.Layouter
{
    public class CircularCloudLayouter : LayouterBase
    {
        private readonly Point center;

        public CircularCloudLayouter(Point center) : base(center)
        {
            this.center = center;
        }

        protected override IEnumerable<Point> GetPoints()
        {
            for (var i = 0; ; i++)
            {
                var x = (int)(i * Math.Cos(i * 0.5)) + center.X;
                var y = (int)(i * Math.Sin(i * -0.5)) + center.Y;
                yield return new Point(x, y);
            }
        }
    }
}
