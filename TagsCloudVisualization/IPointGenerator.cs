using System.Drawing;

namespace TagsCloudVisualization;

public interface IPointGenerator
{
    Point GetNextPoint();
}