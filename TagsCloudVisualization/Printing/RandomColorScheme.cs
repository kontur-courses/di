using System;
using System.Drawing;

namespace TagsCloudVisualization.Printing
{
    internal class RandomColorScheme : IColorScheme
    {
        public Color GetColorBy(Size size)
        {
            var rnd = new Random(size.GetHashCode());
            return Color.FromArgb(
                rnd.Next(byte.MinValue, byte.MaxValue), 
                rnd.Next(byte.MinValue, byte.MaxValue), 
                rnd.Next(byte.MinValue, byte.MaxValue));
        }
    }
}