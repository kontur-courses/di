using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloud.CloudStructure
{
    public class SpiralPointGenerator : IPointGenerator
    {
        public readonly double DAngle;

        public SpiralPointGenerator(double dAngle)
        {
            DAngle = dAngle;
        }

        public IEnumerable<Point> GeneratePoints(Point center)
        {
            var angle = 0.0;
            var curPoint = center;
            while (true)
            {
                yield return curPoint;
                angle += DAngle;
                var x = (int)(angle * Math.Cos(angle) / (2 * Math.PI)) + center.X;
                var y = (int)(angle * Math.Sin(angle) / (2 * Math.PI)) + center.Y;
                curPoint = new Point(x, y);
            }
        }
    }
}
