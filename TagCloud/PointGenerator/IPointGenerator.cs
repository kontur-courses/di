using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.PointGenerator
{
    public interface IPointGenerator
    {
        IEnumerable<PointF> GetPoints(SizeF size);
        PointF Center { get; }
    }
}