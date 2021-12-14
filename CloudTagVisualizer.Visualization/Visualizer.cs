using System;
using System.Collections.Generic;
using System.Drawing;
using Visualization.Layouters;

namespace Visualization
{
    public class Visualizer
    {
        private readonly IWordSizer wordSizer;
        private readonly VisualizerSettings settings;
        private readonly ILayouter layouter;

        public Visualizer(
            IWordSizer wordSizer,
            VisualizerSettings settings,
            ILayouter layouter)
        {
            this.wordSizer = wordSizer;
            this.settings = settings;
            this.layouter = layouter;
        }

        public Bitmap Visualize(string[] words)
        {
            var convertedWords = wordSizer.Convert(words, settings.TextFont.SizeInPoints);
            layouter.Center = (Point) (settings.ImageSize / 2);
            var image = CreateImage(convertedWords);
            return image;
        }

        private Bitmap CreateImage(List<SizedWord> words)
        {
            var imageSize = settings.ImageSize;
            using var bmp = new Bitmap(imageSize.Width, imageSize.Height);
            using var g = Graphics.FromImage(bmp);
            g.FillRectangle(new SolidBrush(settings.BackgroundColor),
                new Rectangle(Point.Empty, settings.ImageSize));
            foreach (var word in words)
            {
                DrawWord(word, g);
            }

            return new Bitmap(bmp, bmp.Size);
        }

        private void DrawWord(SizedWord word, Graphics g)
        {
            var location = layouter.PutNextRectangle(word.WordSize);
            var textBrush = new SolidBrush(settings.TextColor);
            var strokePen = new Pen(settings.StrokeColor);
            var fontWithChangedSize = new Font(settings.TextFont.FontFamily, word.FontSize, settings.TextFont.Style);
            g.DrawRectangle(strokePen, location);
            g.DrawString(word.Word, fontWithChangedSize, textBrush, location);
        }
    }
}