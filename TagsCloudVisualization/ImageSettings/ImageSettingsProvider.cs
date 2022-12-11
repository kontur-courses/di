using System.Drawing;

namespace TagsCloudVisualization.ImageSettings;

public class ImageSettingsProvider : IImageSettingsProvider
{
    private readonly Color backgroundColor;
    private readonly int width;
    private readonly int height;

    public ImageSettingsProvider(Color backgroundColor, int width, int height)
    {
        this.backgroundColor = backgroundColor;
        this.width = width;
        this.height = height;
    }
    public ImageSettings GetSettings()
    {
        return new ImageSettings(backgroundColor, new Size(width, height));
    }
}