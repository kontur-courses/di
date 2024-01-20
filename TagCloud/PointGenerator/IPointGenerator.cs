using System.Drawing;

namespace TagCloud;

public interface IPointGenerator
{
    Point GetNextPoint();
}