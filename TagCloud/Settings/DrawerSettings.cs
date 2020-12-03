using System.Drawing;

namespace TagCloud.Settings
{
    public class DrawerSettings
    {
        public Size ImageSize;
        public Color BackgroundColor;

        public DrawerSettings(Size imageSize, Color backgroundColor)
        {
            ImageSize = imageSize;
            BackgroundColor = backgroundColor;
        }
    }
}