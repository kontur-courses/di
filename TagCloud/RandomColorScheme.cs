using System;
using System.Drawing;
using TagCloud.Interfaces;

namespace TagCloud
{
    public class RandomColorScheme : IColorScheme
    {
        public Color Process(PositionedElement element)
        {
            var random = new Random();
            return Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
        }
    }
}