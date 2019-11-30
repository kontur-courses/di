using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization.Layouters.Spirals
{
    public interface ISpiral
    {
        IEnumerable<PointF> GetSpiralPoints();
    }
}