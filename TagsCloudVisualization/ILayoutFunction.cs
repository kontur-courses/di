using SixLabors.ImageSharp;

namespace TagsCloudVisualization;

public interface ILayoutFunction
{
    public PointF GetNextPoint();
}