using System.Drawing;

namespace TagsCloudContainer.Drawing
{
    public class ImageSettings
    {
        public Size Size { get; }
        public Font TextFont { get; }
        public Color BackgroundColor { get; }
        public Color TextColor { get;}

        public ImageSettings(Size size, Font textFont, Color backgroundColor, Color textColor)
        {
            Size = size;
            TextFont = textFont;
            BackgroundColor = backgroundColor;
            TextColor = textColor;
        }
    }
}