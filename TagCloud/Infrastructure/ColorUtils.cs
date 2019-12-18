using System;
using System.Drawing;

namespace TagCloud.Infrastructure
{
    public class ColorUtils
    {
        public static Color GetRandomColor()
        {
            var rnd = new Random();
            var r = rnd.Next(0, 256);
            var g = rnd.Next(0, 256);
            var b = rnd.Next(0, 256);
            return Color.FromArgb(r, g, b);
        }
    }
}
