using System.Drawing;

namespace TagsCloudVisualization
{
    public interface IPositionGenerator
    {
        Point GetNextPosition();
        Point GetCenter();
    }
}