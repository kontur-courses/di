using System;
using System.Drawing;
using TagCloud.Interfaces;
using TagCloud.IntermediateClasses;

namespace TagCloud
{
    public class RandomColorScheme : IColorScheme
    {
        private const int MaxChannelValue = 256;

        public Color Process(PositionedElement element)
        {
            var random = new Random();
            return Color.FromArgb(
                random.Next(MaxChannelValue),
                random.Next(MaxChannelValue),
                random.Next(MaxChannelValue));
        }
    }
}