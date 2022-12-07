using System.Drawing;

namespace TagsCloudVisualization.ImageSettings;

public interface IImageSettingsProvider
{
    ImageSettings GetSettings(Color backgroundColor, int width, int height);
}