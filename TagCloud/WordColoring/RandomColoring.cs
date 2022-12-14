using System;
using System.Drawing;

namespace TagCloud.WordColoring
{
    public class RandomColoring : IWordColoring
    {
        private static readonly Random random = new Random();

        public Color GetColor(double factor)
        {
            return Color.FromArgb(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));
        }
    }
}
