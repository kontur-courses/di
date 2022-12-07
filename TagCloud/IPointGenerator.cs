using System.Drawing;

namespace TagCloud;

public interface IPointGenerator
{
    public IEnumerable<Point> Generate(Point center);
}