using System.Collections.Generic;
using System.Drawing;

namespace CloudLayouter.Spiral
{
    public interface ISpiral
    {
        IEnumerable<Point> GetPoints(Point center);
    }
}