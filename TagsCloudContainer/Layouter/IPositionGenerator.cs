using System.Drawing;

namespace TagsCloudContainer.Layouter
{
    public interface IPositionGenerator
    {
        Point GetNextPosition();
        Point GetCenter();
    }
}