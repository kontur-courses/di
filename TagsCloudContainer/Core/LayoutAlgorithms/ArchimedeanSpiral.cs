using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Core.LayoutAlgorithms
{
    class ArchimedeanSpiral
    {
        private const double TwoPi = 2 * Math.PI;

        private readonly Point center;

        public ArchimedeanSpiral(Point center)
        {
            this.center = center;
        }

        public IEnumerable<Point> GetPoints(int numberOfTurns, double deltaAngle = TwoPi / 360, int step = 20)
        {
            for (var angle = 0.0; angle < numberOfTurns * Math.PI; angle += deltaAngle)
            {
                yield return new Point(
                    (int) (step * angle * Math.Cos(angle) / TwoPi) + center.X,
                    (int) (step * angle * Math.Sin(angle) / TwoPi) + center.Y
                );
            }
        }
    }
}