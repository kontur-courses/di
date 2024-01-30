using System.Drawing;

namespace TagsCloudVisualization;

public interface IPointGenerator
{
    public string Name { get; }
    Point GetNextPoint();
}