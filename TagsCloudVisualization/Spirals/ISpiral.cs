using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public interface ISpiral
    {
        IEnumerable<Point> GetPoints();
    }
}
