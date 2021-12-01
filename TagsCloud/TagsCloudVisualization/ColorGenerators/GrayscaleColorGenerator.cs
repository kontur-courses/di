using System;
using System.Drawing;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization.ColorGenerators
{
    public class GrayscaleColorGenerator : IColorGenerator
    {
        private readonly Random _random;

        public GrayscaleColorGenerator(Random random)
        {
            _random = random;
        }

        public Color Generate()
        {
            var scale = _random.Next(256);
            return Color.FromArgb(255, scale, scale, scale);
        }
    }
}