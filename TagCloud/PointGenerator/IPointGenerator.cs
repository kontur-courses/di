using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.PointGenerator
{
    public interface IPointGenerator
    {
        IEnumerable<PointF> GetPoints(Size size);
        PointF Center { get; }
    }
}