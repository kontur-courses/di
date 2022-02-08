using SixLabors.ImageSharp;

namespace TagsCloudContainer.Render.CircularCloud;

public interface ISpiral
{
    Point GetNextCurvePoint();
}