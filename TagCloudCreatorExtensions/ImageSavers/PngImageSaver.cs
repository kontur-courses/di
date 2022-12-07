using System.Drawing;
using System.Drawing.Imaging;
using TagCloudCreator.Interfaces;
using TagCloudCreator.Interfaces.Providers;
using TagCloudCreator.Interfaces.Settings;

namespace TagCloudCreatorExtensions.ImageSavers;

public class PngImageSaver : IImageSaver
{
    private readonly IImagePathSettings _imagePathSettings;

    public PngImageSaver(IImagePathSettings imagePathSettings)
    {
        _imagePathSettings = imagePathSettings;
    }

    public string SupportedExtension => ".png";

    public void SaveImage(Image image)
    {
        image.Save(_imagePathSettings.ImagePath, ImageFormat.Png);
    }
}