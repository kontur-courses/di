using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class RandomColorScheme : IColorScheme
    {
        public Color GetColorBy(Size size)
        {
            var rnd = new Random(size.GetHashCode());
            return Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
        }
    }
}