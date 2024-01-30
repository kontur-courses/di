using SixLabors.ImageSharp;

namespace TagsCloudVisualization;

public interface IPointGenerator
{
    PointF GetNextPoint();
}