using System.Drawing;

namespace TagCloud.Abstractions;

public interface IPointGenerator
{
    public IEnumerable<Point> Generate(Point center);
}