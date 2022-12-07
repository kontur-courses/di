using System;
using System.Drawing;

namespace TagCloud
{
    public class GradientColoring : IWordColoring
    {
        private readonly Color colorForMinValue;

        private readonly Color colorForMaxValue;

        private readonly double minValue;

        private readonly double maxValue;

        public GradientColoring(Color minValueColor, Color maxValueColor, double minValue, double maxValue)
        {
            this.colorForMinValue = minValueColor;
            this.colorForMaxValue = maxValueColor;
            this.minValue = minValue;
            this.maxValue = maxValue;
        }

        public Color GetColor(double value = 0)
        {
            var r = GetGradientColorComponent(colorForMinValue.R, colorForMaxValue.R, value);
            var g = GetGradientColorComponent(colorForMinValue.G, colorForMaxValue.G, value);
            var b = GetGradientColorComponent(colorForMinValue.B, colorForMaxValue.B, value);

            return Color.FromArgb( r, g, b);
        }

        private byte GetGradientColorComponent(byte minValueColorComponent, byte maxValueColorComponent, double value)
        {
            return (byte)(minValueColorComponent + (maxValueColorComponent - minValueColorComponent) * (value - minValue) / (maxValue - minValue));
        }
    }
}
