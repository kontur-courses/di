using System.Drawing;

namespace TagsCloudContainer.Drawing
{
    public class ImageSettings
    {
        public Size Size { get; }

        public Font TextFont { get; }
        public float MaxFontSize { get; }
        public float MinFontSize { get; }

        public Color BackgroundColor { get; }
        public Color TextColor { get;}

        public ImageSettings(Size size, Font textFont, float maxFontSize, float minFontSize, Color backgroundColor, Color textColor)
        {
            Size = size;
            TextFont = textFont;
            MaxFontSize = maxFontSize;
            MinFontSize = minFontSize;
            BackgroundColor = backgroundColor;
            TextColor = textColor;
        }
    }
}