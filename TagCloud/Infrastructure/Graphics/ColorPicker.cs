using System;
using System.Collections.Generic;
using System.Drawing;
using TagCloud.Infrastructure.Text.Information;

namespace TagCloud.Infrastructure.Graphics
{
    public class ColorPicker
    {
        private readonly Dictionary<WordType, Color> wordTypeColorMap;
        private readonly Random random;
        private const int MaxColorValue = 256;
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

        private Color GetRandomColor() => Color.FromArgb(random.Next(MaxColorValue), random.Next(MaxColorValue), random.Next(MaxColorValue));
    }
}