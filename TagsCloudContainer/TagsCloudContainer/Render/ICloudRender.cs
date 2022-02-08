using SixLabors.ImageSharp;

namespace TagsCloudContainer.Render;

public interface ICloudRender
{
    Image Render((string Word, int Count)[] words);
}