using System;
using System.Collections.Generic;
using System.Drawing;
using TagCloud.Infrastructure.Text.Information;

namespace TagCloud.Infrastructure.Graphics
{
    public class ColorPicker
    {
        private const int MaxColorValue = 256;
        private readonly Random random;
        private readonly Dictionary<WordType, Color> wordTypeColorMap;

        public ColorPicker(Random random)
        {
            this.random = random;
            wordTypeColorMap = new Dictionary<WordType, Color>();
        }

        public ColorPicker(Random random, Dictionary<WordType, Color> wordTypeColorMap) : this(random)
        {
            this.wordTypeColorMap = wordTypeColorMap;
        }

        public Color GetColor(TokenInfo info)
        {
            if (wordTypeColorMap.TryGetValue(info.WordType, out var color))
                return color;
            color = GetRandomColor();
            wordTypeColorMap.Add(info.WordType, color);
            return color;
        }

        private Color GetRandomColor()
        {
            return Color.FromArgb(random.Next(MaxColorValue), random.Next(MaxColorValue), random.Next(MaxColorValue));
        }
    }
}