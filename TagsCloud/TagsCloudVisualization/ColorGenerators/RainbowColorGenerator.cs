using System;
using System.Drawing;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization.ColorGenerators
{
    public class RainbowColorGenerator : IColorGenerator
    {
        private static readonly Color[] RainbowColors =
        {
            Color.Red,
            Color.Orange,
            Color.Yellow,
            Color.Green,
            Color.LightBlue,
            Color.Blue,
            Color.Purple
        };

        private readonly Random _random;

        public RainbowColorGenerator(Random random)
        {
            _random = random;
        }

        public Color Generate()
        {
            return RainbowColors[_random.Next(RainbowColors.Length)];
        }
    }
}