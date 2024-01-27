using TagsCloudPainterApplication.Properties;

namespace TagsCloudPainterApplication.Infrastructure.Settings;

public class ImageSettings
{
    public ImageSettings(AppSettings settings) 
    {
        Width = settings.imageWidth;
        Height = settings.imageHeight;
    }
    public int Width { get; set; }
    public int Height { get; set; }
}