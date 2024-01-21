using System.Drawing;

namespace TagCloud.PointGenerator;

public interface IPointGenerator
{
    Point GetNextPoint();
}