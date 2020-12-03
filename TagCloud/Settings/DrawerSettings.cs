using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.Settings
{
    public class DrawerSettings
    {
        public Size ImageSize;
        public List<Color> Colors;
        public Color ForegroundColor;
        public Color BackgroundColor;
        public string FontFamily { get; }
        public int MaxFontSize { get; }
        public int MinFontSize { get; }
        public bool OrderByWeight { get; }

        public DrawerSettings(
            Size imageSize, 
            List<Color> colors,
            Color backgroundColor,
            Color foregroundColor, 
            string fontFamily, 
            int minFontSize,
            int maxFontSize,
            bool orderByWeight)
        {
            ImageSize = imageSize;
            Colors = colors;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
            FontFamily = fontFamily;
            MinFontSize = minFontSize;
            MaxFontSize = maxFontSize;
            OrderByWeight = orderByWeight;
        }
    }
}