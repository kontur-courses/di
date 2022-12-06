using System.Drawing;
using System.Drawing.Imaging;
using TagCloudCreator.Interfaces;
using TagCloudCreator.Interfaces.Providers;

namespace TagCloudCreatorExtensions.ImageSavers;

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