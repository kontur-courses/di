using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud.Layouter
{
    public interface ISpiral
    {
        public Point Center { get; }
        public IEnumerable<Point> GetPoints();
        public void SetCenter(Point center);
    }
}
