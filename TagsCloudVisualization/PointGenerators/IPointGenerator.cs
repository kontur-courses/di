using System.Drawing;

namespace TagsCloudVisualization.PointGenerators
{
    public interface IPointGenerator
    {
        Point GetNextPoint();
    }
}