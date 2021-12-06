using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization.PointGenerator
{
    public interface IPointGenerator
    {
        IEnumerable<PointF> GetPoints(Size size);
        PointF Center { get; }
    }
}