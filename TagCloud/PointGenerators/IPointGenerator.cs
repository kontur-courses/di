using System.Drawing;

namespace TagCloud.PointGenerators
{
    public interface IPointGenerator
    {
        Point GetNextPoint();
        Point GetCenterPoint();
    }
}
