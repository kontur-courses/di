using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud.Visualization.PointGenerator
{
    public interface IPointGenerator
    {
        Point Center { get; }
        IEnumerable<Point> GenerateNextPoint();
    }
}