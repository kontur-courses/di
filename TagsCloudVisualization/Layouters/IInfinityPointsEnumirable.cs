using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    internal interface IInfinityPointsEnumerable
    {
        IEnumerable<Point> GetPoints();
    }
}