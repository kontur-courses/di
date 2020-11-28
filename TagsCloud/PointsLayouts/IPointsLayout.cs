using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud
{
    public interface IPointsLayout
    {
        IEnumerable<Point> GetPoints();
    }
}