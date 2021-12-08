using System.Collections.Generic;
using System.Drawing;

namespace CloudTagContainer
{
    public interface ISpiral
    {
        public IEnumerable<Point> GetEnumerator(Point center);
    }
}