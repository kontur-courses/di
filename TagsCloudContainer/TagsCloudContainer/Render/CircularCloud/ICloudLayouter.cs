using SixLabors.ImageSharp;

namespace TagsCloudContainer.Render.CircularCloud;

public interface ICloudLayouter
{
    Rectangle PutNextRectangle(Size size);
}