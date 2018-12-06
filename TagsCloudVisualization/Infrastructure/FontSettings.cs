using System.Drawing;

namespace TagsCloudVisualization
{
    public class FontSettings
    {
        public FontFamily FontFamily { get; } 
        public FontStyle FontStyle { get; }
        public float MaxFontSize { get; }

        public FontSettings(FontFamily fontFamily, FontStyle fontStyle, float maxFontSize = 100f)
        {
            FontFamily = fontFamily;
            FontStyle = fontStyle;
            MaxFontSize = maxFontSize;
        }
    }
}