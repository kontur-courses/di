using System;
using System.Drawing;

namespace TagsCloudContainer.Core.ColoringAlgorithms
{
    public class RandomColoring : IColoringAlgorithm
    {
        private readonly Color[] colors;
        private readonly Random random;

        public RandomColoring()
        {
            colors = new[]
            {
                Color.Black,
                Color.Blue,
                Color.Chocolate,
                Color.Chartreuse,
                Color.DarkGreen,
                Color.DarkRed,
                Color.DeepSkyBlue
            };
            random = new Random();
        }

        public Color GetNextColor()
        {
            var randomNumber = random.Next(0, colors.Length - 1);
            return colors[randomNumber];
        }
    }
}