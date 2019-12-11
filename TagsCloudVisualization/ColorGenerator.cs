using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    public static class ColorGenerator
    {
        public static Color Generate()
        {
            var random = new Random();
            return Color.FromArgb(random.Next(256),
                random.Next(256), random.Next(256));
        }
    }
}