using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization.Infrastructure.Algorithm.Curves
{
    public interface ICurve : IEnumerable<Point>
    {
        Point Center { get; }
    }
}