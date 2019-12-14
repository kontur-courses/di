using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization.Spirals
{
    public interface ISpiral
    {
        IEnumerable<Point> GetPoints();
    }
}
