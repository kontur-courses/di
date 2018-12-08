using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    public static class RandomExtensions
    {
        public static Color GetRandomColor(this Random random)
        {
            return Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
        }
    }
}
