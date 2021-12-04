using System.Drawing;

namespace TagsCloudDrawer.ImageSettings
{
    public interface IImageSettingsProvider
    {
        Color BackgroundColor { get; }
        Size ImageSize { get; }
    }
}