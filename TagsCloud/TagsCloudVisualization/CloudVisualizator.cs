using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using TagsCloudVisualization.Themes;

namespace TagsCloudVisualization
{
    public static class CloudVisualizator
    {
        public static Bitmap Visualize(Theme theme, IEnumerable<Rectangle> rectangles,
            int width = 1000, int height = 1000)
        {
            var random = new Random();
            var result = new Bitmap(width, height);
            using (var graphics = Graphics.FromImage(result))
            {
                graphics.FillRectangle(theme.BackgroundBrush, new Rectangle(0, 0, width, height));
                foreach (var rectangle in rectangles)
                    graphics.FillRectangle(GetRandomBrush(random, theme.RectangleBrushes), rectangle);
            }

            return result;
        }

        private static Brush GetRandomBrush(Random random, ImmutableArray<Brush> brushes)
        {
            return brushes[random.Next(0, brushes.Length)];
        }
    }
}