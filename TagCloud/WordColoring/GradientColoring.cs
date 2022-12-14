using System;
using System.Drawing;

namespace TagCloud.WordColoring
{
    public class GradientColoring : IWordColoring
    {
        public Color MinValueColor { get; set; } = Color.Blue;

        public Color MaxValueColor { get; set; } = Color.DarkRed;

        public double MinValue { get; set; } = 0.0;

        public double MaxValue { get; set; } = 1.0;

        public Color GetColor(double value = 0.0)
        {
            var r = GetGradientColorComponent(MinValueColor.R, MaxValueColor.R, value);
            var g = GetGradientColorComponent(MinValueColor.G, MaxValueColor.G, value);
            var b = GetGradientColorComponent(MinValueColor.B, MaxValueColor.B, value);

            return Color.FromArgb(r, g, b);
        }

        private byte GetGradientColorComponent(byte minValueColorComponent, byte maxValueColorComponent, double value)
        {
            return (byte)(minValueColorComponent + (maxValueColorComponent - minValueColorComponent) * (value - MinValue) / (MaxValue - MinValue));
        }
    }
}
