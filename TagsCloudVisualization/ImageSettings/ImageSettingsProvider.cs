using System.Drawing;

namespace TagsCloudVisualization.ImageSettings;

public class ImageSettingsProvider : IImageSettingsProvider
{
    public ImageSettings GetSettings(Color backgroundColor, int width, int height)
    {
        return new ImageSettings(backgroundColor, new Size(width, height));
    }
}