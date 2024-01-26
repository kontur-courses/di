namespace TagsCloudPainterApplication.Infrastructure.Settings;

public class ImageSettings
{
    public int Width { get; set; } = Properties.Settings.Default.imageWidth;
    public int Height { get; set; } = Properties.Settings.Default.imageHeight;
}