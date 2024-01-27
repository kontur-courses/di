using SixLabors.ImageSharp;

namespace TagsCloudVisualization;

public interface ILayout
{
    const float Accuracy = 1e-3f;
    RectangleF PutNextRectangle(SizeF rectSize);
}