using System.Drawing;

namespace TagCloud.Settings
{
    public class LayoutSettings
    {
        public string FontFamily { get; }
        public int MaxFontSize { get; }
        public int MinFontSize { get; }

        public LayoutSettings(string fontFamily, int minFontSize, int maxFontSize)
        {
            FontFamily = fontFamily;
            MinFontSize = minFontSize;
            MaxFontSize = maxFontSize;
        }
    }
}