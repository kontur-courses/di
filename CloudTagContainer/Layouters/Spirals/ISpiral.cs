using System.Collections.Generic;
using System.Drawing;

namespace CloudTagContainer
{
    public interface ISpiral
    {
        public IEnumerator<Point> GetEnumerator(Point center);
    }
}