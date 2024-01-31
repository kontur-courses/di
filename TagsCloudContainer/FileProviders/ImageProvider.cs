using SixLabors.ImageSharp;

namespace TagsCloudContainer.FileProviders;

public class ImageProvider: IImageProvider
{
    public void SaveImage(Image image, string filePath)
    {
        image.Save(filePath);
    }
}