using System;
using System.Drawing;

namespace TagsCloudVisualization.CloudPainters
{
    public static class MultiColorPainterTools
    {
        public static Color GetRandomColor()
        {
            var random = new Random();
            return Color.FromArgb(random.Next(256),
                random.Next(256), random.Next(256));
        }
    }
}