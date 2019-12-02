using System.Drawing;

namespace TagCloud
{
    public interface IAlgorithm
    {
        Point GetNextCoordinate();
    }
}
