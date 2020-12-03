using System;
using System.Drawing;

namespace WordCloudGenerator
{
    public class RandomChoicePalette : IPalette
    {
        private readonly Color[] colors;

        public RandomChoicePalette(Color[] colors)
        {
            this.colors = colors;
        }

        public Color GetNextColor()
        {
            var rnd = new Random();
            return colors[rnd.Next(0, colors.Length)];
        }
    }
}