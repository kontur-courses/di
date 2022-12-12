using System;
using System.Drawing;

namespace TagsCloudVisualization.Settings
{
    public class PaletteSettings : IPaletteSettings
    {
        private readonly FontSettings fontSettings;

        public PaletteSettings(FontSettings fontSettings)
        {
            this.fontSettings = fontSettings;
        }

        public Color PrimaryColor { get; set; } = Color.Yellow;
        public Color SecondaryColor { get; set; } = Color.Red;
        public Color BackgroundColor { get; set; } = Color.Black;

        public Color GetColorAccordingSize(float emSize)
        {
            if (emSize > fontSettings.MaxEmSize)
                throw new ArgumentException("size > maxSize");

            var difference = fontSettings.MaxEmSize - fontSettings.MinEmSize;
            var vector = (R: PrimaryColor.R - SecondaryColor.R,
                G: PrimaryColor.G - SecondaryColor.G,
                B: PrimaryColor.B - SecondaryColor.B);

            var length = Math.Sqrt(Math.Pow(vector.R, 2) + Math.Pow(vector.G, 2) + Math.Pow(vector.B, 2));

            if (length == 0) return SecondaryColor;

            var r = vector.R;
            var g = vector.G;
            var b = vector.B;

            var coef = (emSize - fontSettings.MinEmSize) / difference;

            return Color.FromArgb(
                Math.Min(SecondaryColor.R + (int)(r * coef), 255),
                Math.Min(SecondaryColor.G + (int)(g * coef), 255),
                Math.Min(SecondaryColor.B + (int)(b * coef), 255));
        }
    }
}