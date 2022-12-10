using System;
using System.Drawing;

namespace TagsCloudVisualization.Settings
{
    public class PaletteSettings
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
            var difference = fontSettings.MaxEmSize - fontSettings.MinEmSize;
            var vector = (R: PrimaryColor.R - SecondaryColor.R,
                G: PrimaryColor.G - SecondaryColor.G,
                B: PrimaryColor.B - SecondaryColor.B);
            var length = Math.Sqrt(Math.Pow(vector.R, 2) + Math.Pow(vector.G, 2) + Math.Pow(vector.B, 2));
            if (length == 0) return Color.White;

            var r = vector.R;
            var g = vector.G;
            var b = vector.B;

            var coef = (emSize - fontSettings.MinEmSize) / difference;

            return Color.FromArgb(SecondaryColor.R + (int)(r * coef),
                SecondaryColor.G + (int)(g * coef),
                SecondaryColor.B + (int)(b * coef));
        }
    }
}