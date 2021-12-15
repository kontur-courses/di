using System;
using System.Drawing;

namespace TagsCloudVisualization.Default
{
    public class RandomTagColor : ITokenColorChooser
    {
        private Random random = new Random();
        public Color GetTokenColor(Token token)
        {
            return Color.FromArgb(random.Next(0,200), random.Next(0,200), random.Next(0,200));
        }
    }
}