using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloudContainer.Api;

namespace TagCloudContainer
{
    public class TagCloudVisualizer : IWordVisualizer
    {
        private IWordCloudLayouter Layouter;

        public TagCloudVisualizer(IWordCloudLayouter layouter)
        {
            Layouter = layouter;
        }

        public Image CreateImageWithWords(IEnumerable<string> words, DrawingOptions options)
        {
            var wordsAndCounts = WordCounter.CreateWordOccurrencesDictionary(words);
            var wordsAndRectangles = Layouter.AddWords(wordsAndCounts);

            var bmp = CreateSizedBitmapForLayouter(Layouter);
            var graphics = Graphics.FromImage(bmp);

            FillBackground(graphics, bmp, options);
            WriteWordsOnImage(wordsAndRectangles, options, graphics, bmp);

            graphics.Flush();
            return bmp;
        }

        private static void WriteWordsOnImage(IReadOnlyDictionary<string, Rectangle> pairs,
            DrawingOptions options, Graphics graphics, Image bmp)
        {
            foreach (var (word, rect) in pairs)
            {
                rect.Offset(+bmp.Width / 2, +bmp.Height / 2);
                graphics.DrawString(word, options.Font, options.WordBrush, rect.X, rect.Y);
            }
        }

        private static Bitmap CreateSizedBitmapForLayouter(ILayoutProvider layouter)
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