using SixLabors.ImageSharp;

namespace TagsCloudContainer.FileProviders;

public interface IImageProvider
{
    public void SaveImage(Image image, string filePath);
}