using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization
{
    public class Spiral : ISpiral
    {
        private readonly PointF center;
        private readonly double step;
        private readonly IEnumerator<PointF> spiralPointsPath;

        public Spiral(PointF center, double step = 0.01)
        {
            this.center = center;
            this.step = step;
            spiralPointsPath = GetSpiralPath()
                .GetEnumerator();
        }

        public PointF GetNextPointOnSpiral()
        {
            spiralPointsPath.MoveNext();
            return spiralPointsPath.Current;
        }

        private IEnumerable<PointF> GetSpiralPath()
        {
            var a = 0;
            while (true)
            {
                var x = (float)(center.X + step * a * Math.Cos(a));
                var y = (float)(center.Y + step * a * Math.Sin(a));
                yield return new PointF(x, y);

                a += 2;
            }
        }
    }
}
