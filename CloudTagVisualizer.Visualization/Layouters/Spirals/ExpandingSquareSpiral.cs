using System.Collections.Generic;
using System.Drawing;

namespace Visualization.Layouters.Spirals
{
    public class ExpandingSquareSpiral : ISpiral
    {
        public IEnumerable<Point> GetEnumerator(Point center)
        {
            yield return center;

            var chebyshevDistance = 1;
            while (true)
            {
                foreach (var point in GetUpperPoints(chebyshevDistance, center))
                    yield return point;

                foreach (var point in GetRightPoints(chebyshevDistance, center))
                    yield return point;

                foreach (var point in GetLowerPoints(chebyshevDistance, center))
                    yield return point;

                foreach (var point in GetLeftPoints(chebyshevDistance, center))
                    yield return point;

                chebyshevDistance++;
            }
        }

        private IEnumerable<Point> GetUpperPoints(int chebyshevDistance, Point center)
        {
            for (var dx = -chebyshevDistance; dx <= chebyshevDistance; dx++)
            {
                yield return new Point(center.X + dx, center.Y - chebyshevDistance);
            }
        }

        private IEnumerable<Point> GetRightPoints(int chebyshevDistance, Point center)
        {
            for (var dy = -chebyshevDistance + 1; dy <= chebyshevDistance; dy++)
            {
                yield return new Point(center.X + chebyshevDistance, center.Y + dy);
            }
        }

        private IEnumerable<Point> GetLowerPoints(int chebyshevDistance, Point center)
        {
            for (var dx = -chebyshevDistance + 1; dx <= chebyshevDistance; dx++)
            {
                yield return new Point(center.X - dx, center.Y + chebyshevDistance);
            }
        }

        private IEnumerable<Point> GetLeftPoints(int chebyshevDistance, Point center)
        {
            for (var dy = -chebyshevDistance + 1; dy < chebyshevDistance; dy++)
            {
                yield return new Point(center.X - chebyshevDistance, center.Y - dy);
            }
        }
    }
}