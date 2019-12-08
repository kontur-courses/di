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
            var wordsAndCounts = WordCounter.CreateWordOccurrencesDictionary(words);
            var wordsAndRectangles = layouter.AddWords(wordsAndCounts);

            var bmp = CreateSizedBitmap();
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
                var count = counts[word];
                var brush = wordBrushProvider.CreateBrushForWord(word, count);
                rect.Offset(+bmp.Width / 2, +bmp.Height / 2);
                graphics.DrawString(word, options.Font, brush, rect.X, rect.Y);
            }
        }

        private Bitmap CreateSizedBitmap()
        {
            int maxX = layouter.Layout.Select(r => r.Right).Max();
            int minX = layouter.Layout.Select(r => r.Left).Min();
            int maxY = layouter.Layout.Select(r => r.Bottom).Max();
            int minY = layouter.Layout.Select(r => r.Top).Min();

            int bmpWidth = maxX - minX;
            int bmpHeight = maxY - minY;

            return new Bitmap(bmpWidth, bmpHeight);
        }

        private static void FillBackground(Graphics graphics, Image bmp, DrawingOptions options)
        {
            graphics.FillRegion(options.BackgroundBrush, new Region(new Rectangle(0, 0, bmp.Width, bmp.Width)));
        }
    }
}