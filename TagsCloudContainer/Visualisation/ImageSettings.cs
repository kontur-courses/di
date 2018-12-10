using System.Drawing;

namespace TagsCloudContainer.Visualisation
{
    public class ImageSettings
    {
        public FontFamily FontFamily { get; }
        public Size ImageSize { get; }
        public Color TextColor { get; }

        public ImageSettings(FontFamily fontFamily, Size imageSize, Color textColor)
        {
            FontFamily = fontFamily;
            ImageSize = imageSize;
            TextColor = textColor;
        }

        public ImageSettings(IUI ui)
        {
            FontFamily = FontFamily.GenericMonospace;
            ImageSize = ui.ImageSize;
            TextColor = ui.TextColor;
        }
    }
}