using System;
using System.Drawing;

namespace TagsCloudVisualization.Extensions
{
    public static class ColorExtensions
    {
        public static Color GetRandomColor()
        {
            var random = new Random();
            return Color.FromArgb(random.Next(256),
                random.Next(256), random.Next(256));
        }
    }
}