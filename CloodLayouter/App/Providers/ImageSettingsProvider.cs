using CloodLayouter.Infrastructer;

namespace CloodLayouter.App
{
    public class ImageSettingsProvider : IProvider<ImageSettings>
    {
        public ImageSettings Get()
        {
            return new ImageSettings();
        }
    }
}