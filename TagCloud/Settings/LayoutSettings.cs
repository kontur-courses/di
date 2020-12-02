using System.Drawing;

namespace TagCloud.Settings
{
    public class LayoutSettings
    {
        public FontFamily FontFamily { get; }
        public int MaxFontSize { get; }
        public int MinFontSize { get; }

        public LayoutSettings(FontFamily fontFamily, int minFontSize, int maxFontSize)
        {
            FontFamily = fontFamily;
            MinFontSize = minFontSize;
            MaxFontSize = maxFontSize;
        }
    }
}