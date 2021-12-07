using System.Collections.Generic;
using System.Drawing;

namespace CloudTagContainer
{
    public class ExpandingSquare : ISpiral
    {
        private readonly Point center;

        public IEnumerator<Point> GetEnumerator(Point center)
        {
            yield return center;

            var chebyshevDistance = 1;
            while (true)
            {
                foreach (var point in GetUpperPoints(chebyshevDistance))
                    yield return point;

                foreach (var point in GetRightPoints(chebyshevDistance))
                    yield return point;

                foreach (var point in GetLowerPoints(chebyshevDistance))
                    yield return point;

                foreach (var point in GetLeftPoints(chebyshevDistance))
                    yield return point;

                chebyshevDistance++;
            }
        }

        private IEnumerable<Point> GetUpperPoints(int chebyshevDistance)
        {
            for (var dx = -chebyshevDistance; dx <= chebyshevDistance; dx++)
            {
                yield return new Point(center.X + dx, center.Y - chebyshevDistance);
            }
        }

        private IEnumerable<Point> GetRightPoints(int chebyshevDistance)
        {
            for (var dy = -chebyshevDistance + 1; dy <= chebyshevDistance; dy++)
            {
                yield return new Point(center.X + chebyshevDistance, center.Y + dy);
            }
        }

        private IEnumerable<Point> GetLowerPoints(int chebyshevDistance)
        {
            for (var dx = -chebyshevDistance + 1; dx <= chebyshevDistance; dx++)
            {
                yield return new Point(center.X - dx, center.Y + chebyshevDistance);
            }
        }

        private IEnumerable<Point> GetLeftPoints(int chebyshevDistance)
        {
            for (var dy = -chebyshevDistance + 1; dy < chebyshevDistance; dy++)
            {
                yield return new Point(center.X - chebyshevDistance, center.Y - dy);
            }
        }
    }
}