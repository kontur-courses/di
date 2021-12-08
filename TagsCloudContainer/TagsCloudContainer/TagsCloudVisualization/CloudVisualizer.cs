using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.TagsCloudVisualization
{
    public static class CloudVisualizer
    {
        public static Bitmap Draw(CircularCloudLayouter layouter, List<Color> colors, Color backgroundColor)
        {
            var bitmap = new Bitmap(layouter.Size.Width, layouter.Size.Height);
            var graphics = Graphics.FromImage(bitmap);
            graphics.Clear(backgroundColor);
            var rnd = new Random();
            foreach (var rectangle in layouter.Rectangles)
            {
                var pen = new Pen(colors[rnd.Next(colors.Count)], 0);
                graphics.DrawRectangle(pen, rectangle);
            }

            return bitmap;
        }
    }
}