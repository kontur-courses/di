using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.Drawing.Drawing2D;
using TagsCloudVisualization.Styling;
using TagsCloudVisualization.Styling.Themes;

namespace TagsCloudVisualization.Visualizers
{
    public class TextNoRectanglesVisualizer : ICloudVisualizer
    {
        public Bitmap Visualize(Theme theme, IEnumerable<RectangleF> rectangles,
            int width = 1000, int height = 1000)
        {
            var result = new Bitmap(width, height);
            var random = new Random();
            using (var graphics = Graphics.FromImage(result))
            {
                graphics.FillRectangle(theme.BackgroundBrush, new RectangleF(0, 0, width, height));
                foreach (var rectangle in rectangles)
                    graphics.FillRectangle(GetRandomBrush(random, theme.RectangleBrushes), rectangle);
                return result;
            }
        }

        public Bitmap Visualize(Style style, IEnumerable<Tag> tags,
            int width = 1000, int height = 1000)
        {
            var result = new Bitmap(width, height);
            using (var graphics = Graphics.FromImage(result))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.FillRectangle(style.Theme.BackgroundBrush, new RectangleF(0, 0, width, height));
                foreach (var tag in tags)
                {
                    var font = new Font(style.FontProperties.Name, style.GetWordSize(tag.Count));
                    var state = graphics.Save();
                    graphics.TranslateTransform(tag.Area.Left, tag.Area.Top - tag.Area.Height / 2);
                    graphics.DrawString(tag.Word, font, style.Theme.GetTagBrush(tag), PointF.Empty);
                    graphics.Restore(state);
                }

                return result;
            }
        }

        private Brush GetRandomBrush(Random random, ImmutableArray<Brush> brushes)
        {
            return brushes[random.Next(0, brushes.Length)];
        }
    }
}