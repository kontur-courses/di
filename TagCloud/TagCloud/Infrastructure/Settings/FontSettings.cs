using System;
using System.Drawing;

namespace TagCloud
{
    public class FontSettings
    {
        public readonly FontFamily Family;
        public readonly FontStyle Style;
        public readonly Color Color;
        public readonly float DefaultSize;
        public readonly float CountMultiplier;

        public FontSettings(FontFamily family, FontStyle style, Color color, float size, float multiplier)
        {
            Family = family ?? throw new ArgumentNullException();
            Style = style;
            Color = color;
            DefaultSize = size;
            CountMultiplier = multiplier;
        }

        public static FontSettings GetDefaultSettings() =>
            new FontSettings(FontFamily.GenericSansSerif, FontStyle.Bold, Color.Black, 12, 1.5f);
    }
}
