using System.Collections.Generic;
using System.Drawing;

namespace Visualization
{
    public interface ISpiral
    {
        public IEnumerable<Point> GetEnumerator(Point center);
    }
}