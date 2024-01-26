using SixLabors.ImageSharp;

namespace TagsCloudVisualization;

public interface IPointGenerator
{
    public PointF GetNextPoint();
}