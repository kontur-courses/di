using System.Drawing;

namespace TagCloud.PointGenerator;

public interface IPointGenerator
{
    string GeneratorName { get; }
    Point GetNextPoint();
}