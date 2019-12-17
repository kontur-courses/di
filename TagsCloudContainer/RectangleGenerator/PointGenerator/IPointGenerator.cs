using System.Drawing;

namespace TagsCloudContainer.RectangleGenerator.PointGenerator
{
    public interface IPointGenerator
    {
        Point Center { get; }
        Point GetNextPoint();
    }
}