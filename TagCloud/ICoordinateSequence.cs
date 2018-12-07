using System.Drawing;

namespace TagsCloud
{
    public interface ICoordinateSequence
    {
        Point GetNextCoordinate();
    }
}