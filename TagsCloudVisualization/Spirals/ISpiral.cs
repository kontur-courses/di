using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization.Spirals
{
    public interface ISpiral
    {
        string Name { get; }
        IEnumerable<Point> GetPoints();
    }
}
