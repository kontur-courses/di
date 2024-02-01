using TagsCloudPainterApplication.Properties;

namespace TagsCloudPainterApplication.Infrastructure.Settings.Image;

public class ImageSettings : IImageSettings
{
    public ImageSettings(IAppSettings settings)
    {
        Width = settings.ImageWidth;
        Height = settings.ImageHeight;
    }

    public int Width { get; set; }
    public int Height { get; set; }
}