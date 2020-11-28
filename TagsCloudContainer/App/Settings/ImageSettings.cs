using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudContainer.App.Settings
{
    internal class ImageSettings
    {
        public readonly string FontName;
        public readonly ImageFormat Format;
        public readonly Size ImageSize;
        public readonly Color TextColor;

        public ImageSettings(Size imageSize, string fontName, Color textColor, ImageFormat imageFormat)
        {
            ImageSize = imageSize;
            FontName = fontName;
            TextColor = textColor;
            Format = imageFormat;
        }
    }
}