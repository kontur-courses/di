using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public interface IInfinityPointsEnumerable
    {
        IEnumerable<Point> GetPoints();
    }
}