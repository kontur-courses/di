using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    class ArchimedeanSpiral : ISpiral
    {
        const int stepSize = 2;
        const double stepValueInRadians = 0.05;
        const double eccentricity = 1;

        public IEnumerable<Point> GetPoints()
        {
            double angle = 0;
            Point curentPosition = new Point(0, 0);
            yield return curentPosition;

            while (true)
            {
                angle += stepValueInRadians * stepSize * -1;

                int x = (int)(eccentricity * angle * Math.Cos(angle));
                int y = (int)(angle * Math.Sin(angle));

                var nextPosition = new Point(x, y);

                if (nextPosition == curentPosition) continue;
                else curentPosition = nextPosition;

                yield return curentPosition;
            }
        }
    }
}
