using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Algorithm.Curves
{
    public interface ICurve : IEnumerable<Point>
    {
        Point Center { get; }
    }
}
