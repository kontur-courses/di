using System.Drawing;

namespace TagCloud
{
    public class FontSettings
    {
        public readonly FontFamily FontFamily;
        public readonly FontStyle Style;
        public readonly Color Color;
        public readonly float DefaultSize;
        public readonly float CountMultiplier;

        public FontSettings(string fontFamilyName, FontStyle style, Color color, float size, float multiplier)
        {
            FontFamily = new FontFamily(fontFamilyName);
            Style = style;
            Color = color;
            DefaultSize = size;
            CountMultiplier = multiplier;
        }

        public static FontSettings GetDefaultSettings() =>
            new FontSettings("Arial", FontStyle.Bold, Color.Black, 12, 1.5f);
    }
}
