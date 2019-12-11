using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloudContainer.Api;

namespace TagCloudContainer.Implementations
{
    public class TagCloudVisualizer : IWordVisualizer
    {
        private readonly IWordCloudLayouter layouter;
        private readonly IWordBrushProvider wordBrushProvider;

        public TagCloudVisualizer(IWordCloudLayouter layouter, IWordBrushProvider wordBrushProvider)
        {
            this.layouter = layouter;
            this.wordBrushProvider = wordBrushProvider;
        }

        public Image CreateImageWithWords(IEnumerable<string> words, DrawingOptions options)
        {
            List<Rectangle> layout = new List<Rectangle>();
            var wordsAndCounts = WordCounter.CreateWordOccurrencesDictionary(words);
            var wordsAndRectangles = layouter.AddWords(wordsAndCounts, layout);

            var bmp = layout.CreateSizedBitmap();
            var graphics = Graphics.FromImage(bmp);

            FillBackground(graphics, bmp, options);
            WriteWordsOnImage(wordsAndRectangles, wordsAndCounts, options, graphics, bmp);

            graphics.Flush();
            return bmp;
        }

        private void WriteWordsOnImage(IReadOnlyDictionary<string, Rectangle> rectangles,
            IReadOnlyDictionary<string, int> counts,
            DrawingOptions options, Graphics graphics, Image bmp)
        {
            foreach (var (word, rect) in rectangles)
            {
                rect.Offset(+bmp.Width / 2, +bmp.Height / 2);
                graphics.TranslateTransform(rect.X, rect.Y);
                var count = counts[word];
                var brush = wordBrushProvider.CreateBrushForWord(word, count);
                graphics.ScaleTransform(count, count);
                graphics.DrawString(word, options.Font, brush, 0, 0);
                graphics.ScaleTransform(1f / count, 1f / count);
                graphics.TranslateTransform(-rect.X, -rect.Y);
            }
        }


        private static void FillBackground(Graphics graphics, Image bmp, DrawingOptions options)
        {
            graphics.FillRegion(options.BackgroundBrush, new Region(new Rectangle(0, 0, bmp.Width, bmp.Width)));
        }
    }
}