using SixLabors.ImageSharp;

namespace TagsCloudVisualization;

public interface ILayout
{
    public RectangleF PutNextRectangle(SizeF rectSize);
}