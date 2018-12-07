using System.Drawing;

namespace TagsCloudVisualization
{
    public class ImageSettings
    {
        public FontFamily FontFamily { get; }
        public Size PictureSize { get; }
        public Color TextColor { get; }

        public ImageSettings(FontFamily fontFamily, Size pictureSize, Color textColor)
        {
            FontFamily = fontFamily;
            PictureSize = pictureSize;
            TextColor = textColor;
        }
    }
}