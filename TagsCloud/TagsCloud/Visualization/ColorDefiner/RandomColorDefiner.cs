using System;
using System.Drawing;

namespace TagsCloud.Visualization.ColorDefiner
{
    public class RandomColorDefiner : IColorDefiner
    {
        private readonly Random _random = new Random();

        Color IColorDefiner.DefineColor(int frequency)
        {
            return Color.FromArgb(_random.Next(256), _random.Next(256), _random.Next(256));
        }
    }
}