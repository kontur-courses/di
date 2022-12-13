using System.Drawing;

namespace TagCloud.PointGenerator
{
    public interface IPointGenerator
    {
        Point CentralPoint { get; }
        Point GetNextPoint();
    }
}
