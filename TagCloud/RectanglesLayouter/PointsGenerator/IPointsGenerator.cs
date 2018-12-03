using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.RectanglesLayouter.PointsGenerator
{
    public interface IPointsGenerator
    {
        IEnumerable<Point> GetPoints();
    }
}