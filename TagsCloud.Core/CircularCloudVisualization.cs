using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud.Core
{
    public static class CircularCloudVisualization
    {
        private static readonly Random Random = new Random();

        public static Bitmap CreateImage(List<string> words, List<Rectangle> rectangles,
            int imageWidth, int imageHeight, List<Color> colors = null, string fontName = "Arial")
        {
            var image = new Bitmap(imageWidth, imageHeight);
            for (var i = 0; i < rectangles.Count; ++i)
            {
                var drawFormat = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                Graphics.FromImage(image).DrawRectangle(new Pen(Color.Black), rectangles[i]);
                Graphics.FromImage(image).DrawString(words[i], new Font(fontName, (int)(rectangles[i].Height / 1.3)),
                    new SolidBrush(GetRandomColor(colors)), rectangles[i], drawFormat);
            }
            return image;
        }

        private static Color GetRandomColor(List<Color> colors)
        {
            if (colors == null || colors.Count == 0)
                return Color.FromArgb(Random.Next(0, 255), Random.Next(0, 255), Random.Next(0, 255));
            return colors[Random.Next(0, colors.Count)];
        }
    }
}
