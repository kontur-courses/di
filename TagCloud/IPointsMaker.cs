using System.Collections.Generic;
using System.Drawing;

namespace TagCloud
{
    public interface IPointsMaker
    {
        IEnumerable<Point> GenerateNextPoint(Point center, double spiralStep);
    }
}