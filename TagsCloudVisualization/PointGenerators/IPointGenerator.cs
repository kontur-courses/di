using System.Drawing;

namespace TagsCloudVisualization;

public interface IPointGenerator
{
    public Algorithm Name { get; }
    Point GetNextPoint();
}