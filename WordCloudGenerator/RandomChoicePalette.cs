using System;
using System.Drawing;

namespace WordCloudGenerator
{
    public class RandomChoicePalette : IPalette
    {
        private readonly Color[] colors;

        public RandomChoicePalette(Color[] colors, Color backgroundColor)
        {
            this.colors = colors;
            BackgroundColor = backgroundColor;
        }

        public Color BackgroundColor { get; }

        public Color GetNextColor()
        {
            var rnd = new Random();
            return colors[rnd.Next(0, colors.Length)];
        }
    }
}