using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloud.Layouter.Base;

namespace TagsCloud.Layouter
{
    public class CircularCloudLayouter : LayouterBase
    {
        private readonly Point center;
        private int parameter;

        public CircularCloudLayouter(Point center) : base(center)
        {
            this.center = center;
        }

        protected override IEnumerable<Point> GetPoints()
        {
            while (true)
            {
                var x = (int)(parameter * Math.Cos(parameter * 0.5)) + center.X;
                var y = (int)(parameter * Math.Sin(parameter * -0.5)) + center.Y;
                parameter++;
                yield return new Point(x, y);
            }
        }
    }
}
