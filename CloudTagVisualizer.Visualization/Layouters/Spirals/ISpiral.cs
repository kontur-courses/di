using System.Collections.Generic;
using System.Drawing;

namespace Visualization.Layouters.Spirals
{
    public interface ISpiral
    {
        public IEnumerable<Point> GetEnumerator(Point center);
    }
}