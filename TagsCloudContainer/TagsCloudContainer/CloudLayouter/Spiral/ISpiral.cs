using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.CloudLayouter.Spiral
{
    public interface ISpiral
    {
        IEnumerable<Point> GetPoints(Point center);
    }
}