using System;
using System.Drawing;

namespace TagCloud.Templates.Colors
{
    public class RandomColorGenerator : IColorGenerator
    {
        private static Random random = new();
        public Color GetColor(string word)
        {
            var r = random.Next(255);
            var g = random.Next(255);
            var b = random.Next(255);
            return Color.FromArgb(r, g, b);
        }
    }
}