using System.Collections.Generic;
using System.Drawing;

namespace TagCloud
{
    public interface IAlgorithm
    {
        IEnumerable<Point> GetCoordinates();
    }
}