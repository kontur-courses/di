using System.Drawing.Imaging;
using TagCloudApp.Abstractions;

namespace TagCloudApp.Implementations;

public class PngImageSaver : IImageSaver
{
    private readonly IImagePathProvider _imagePathProvider;

    public PngImageSaver(IImagePathProvider imagePathProvider)
    {
        _imagePathProvider = imagePathProvider;
    }

    public string SupportedExtension => ".png";

    public void SaveImage(Image image)
    {
        image.Save(_imagePathProvider.ImagePath, ImageFormat.Png);
    }
}