using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization.Spirals
{
    public class ArchimedeanSpiral : ISpiral
    {
        const int StepSize = 2;
        const double StepValueInRadians = 0.05;
        const double Eccentricity = 1;

        public IEnumerable<Point> GetPoints()
        {
            double angle = 0;
            var currentPosition = new Point(0, 0);
            yield return currentPosition;

            while (true)
            {
                angle += StepValueInRadians * StepSize * -1;

                int x = (int)(Eccentricity * angle * Math.Cos(angle));
                int y = (int)(angle * Math.Sin(angle));

                var nextPosition = new Point(x, y);

                if (Equals(nextPosition, currentPosition)) continue;
                currentPosition = nextPosition;

                yield return currentPosition;
            }
        }
    }
}
