using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    interface ISpiral
    {
        IEnumerable<Point> GetPoints();
    }
}
