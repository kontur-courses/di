using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Tests.Extensions;

namespace TagsCloudVisualization.Tests
{
    public class DistantPointFinder
    {
        private readonly Point _center;

        public DistantPointFinder(Point center)
        {
            _center = center;
        }

        public Point GetDistantPoint(IEnumerable<Point> points)
        {
            return points.Aggregate((best, current) =>
                _center.DistanceTo(current) > _center.DistanceTo(best) ? current : best);
        }
    }
}