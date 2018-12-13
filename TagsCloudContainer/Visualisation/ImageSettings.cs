using System.Drawing;
using TagsCloudContainer.UI;

namespace TagsCloudContainer.Visualisation
{
    public class ImageSettings
    {
        public FontFamily FontFamily { get; }
        public Size ImageSize { get; }
        public Color TextColor { get; }
        public IColorManager ColorManager { get; }

        public ImageSettings(FontFamily fontFamily, Size imageSize, Color textColor)
        {
            FontFamily = fontFamily;
            ImageSize = imageSize;
            TextColor = textColor;
        }

        public ImageSettings(FontFamily fontFamily, Size imageSize, IColorManager colorManager)
        {
            FontFamily = fontFamily;
            ImageSize = imageSize;
            ColorManager = colorManager;
        }

        public ImageSettings(IUI ui)
        {
            FontFamily = FontFamily.GenericMonospace;
            ImageSize = ui.ImageSize;
            TextColor = ui.TextColor;
        }
    }
}