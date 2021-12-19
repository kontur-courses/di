using System;
using System.Drawing;

namespace TagCloud2
{
    public class RandomColoringAlgorithm : IColoringAlgorithm
    {
        private readonly Random random = new();
        public Color GetColor(Rectangle rectangle)
        {
            
            return Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
        }
    }
}
