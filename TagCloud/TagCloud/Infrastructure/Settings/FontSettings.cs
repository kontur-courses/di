using System.Drawing;

namespace TagCloud
{
    public class FontSettings
    {
        public FontFamily FontFamily { get; set; }
        public FontStyle Style { get; set; }
        public float DefaultSize { get; set; }
        public float CountMultiplier { get; set; }

        public FontSettings(string fontFamilyName, FontStyle style, float size, float multiplier)
        {
            FontFamily = new FontFamily(fontFamilyName);
            Style = style;
            DefaultSize = size;
            CountMultiplier = multiplier;
        }

        public static FontSettings GetDefaultSettings() =>
            new FontSettings("Arial", FontStyle.Bold, 12, 1.5f);
    }
}
