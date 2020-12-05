using System;
using System.Drawing;

namespace TagCloudCreator
{
    public class FullRandomColorSelector : IColorSelector
    {
        private readonly Random random = new Random();

        public Color GetColor(DrawingWord word)
        {
            return Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
        }
    }
}