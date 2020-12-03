using System.Drawing;

namespace TagCloud.Settings
{
    public class DrawerSettings
    {
        public Size ImageSize;
        public Color BackgroundColor;
        public string FontFamily { get; }
        public int MaxFontSize { get; }
        public int MinFontSize { get; }

        public DrawerSettings(
            Size imageSize, 
            Color backgroundColor, 
            string fontFamily, 
            int minFontSize,
            int maxFontSize)
        {
            ImageSize = imageSize;
            BackgroundColor = backgroundColor;
            FontFamily = fontFamily;
            MinFontSize = minFontSize;
            MaxFontSize = maxFontSize;
        }
    }
}