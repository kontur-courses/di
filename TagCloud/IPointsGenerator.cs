using System.Collections.Generic;
using System.Drawing;

namespace TagCloud
{
    public interface IPointsGenerator
    {
        IEnumerable<Point> GetPoints();
    }
}