using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace TagCloudContainer
{
    public static class LayouterVisualizer
    {
        public static TagCloudBitmapContainer CreateCloudWithWordsFromFile(IEnumerable<string> words,
            ICircularCloudLayouter rectangleLayouter,
            IWordCloudLayouter layouter,
            Brush backgroundBrush,
            Brush wordBrush,
            Font font)
        {
            return new TagCloudBitmapContainer(CreateBitmapOfCloudWithWords(words, rectangleLayouter, layouter,
                backgroundBrush, wordBrush, font));
        }

        public static void SaveBitmapWithRectanglesFromLayout(ICircularCloudLayouter layouter,
            string outputFile,
            Brush brush,
            Pen pen)
        {
            var bmp = CreateSizedBitmapForLayouter(layouter);
            var graphics = Graphics.FromImage(bmp);
            FillBackground(graphics, bmp, brush);

            DrawRectanglesAtBitmap(layouter, graphics, bmp, pen);

            graphics.Flush();
            bmp.Save(outputFile);
        }


        public static Bitmap CreateSizedBitmapForLayouter(ICircularCloudLayouter layouter)
        {
            int maxX = layouter.Layout.Select(r => r.Right).Max();
            int minX = layouter.Layout.Select(r => r.Left).Min();
            int maxY = layouter.Layout.Select(r => r.Bottom).Max();
            int minY = layouter.Layout.Select(r => r.Top).Min();

            int bmpWidth = maxX - minX;
            int bmpHeight = maxY - minY;

            return new Bitmap(bmpWidth, bmpHeight);
        }

        private static void FillBackground(Graphics graphics, Bitmap bmp, Brush brush)
        {
            graphics.FillRegion(brush, new Region(new Rectangle(0, 0, bmp.Width, bmp.Width)));
        } 

        private static void DrawRectanglesAtBitmap(
            ICircularCloudLayouter layouter,
            Graphics graphics,
            Bitmap bmp,
            Pen pen)
        {
            foreach (var rect in layouter.Layout)
            {
                rect.Offset(new Point(bmp.Width / 2, bmp.Height / 2));
                graphics.DrawRectangle(pen, rect);
            }
        }

        private static Bitmap CreateBitmapOfCloudWithWords(IEnumerable<string> words,
            ICircularCloudLayouter rectangleLayouter,
            IWordCloudLayouter layouter,
            Brush backgroundBrush,
            Brush wordBrush,
            Font font)
        {
            var wordsAndCounts = WordProcessor.CountWordOccurrences(words);
            var wordsAndRectangles = layouter.AddWords(wordsAndCounts).ToList();

            var bmp = CreateSizedBitmapForLayouter(rectangleLayouter);
            var graphics = Graphics.FromImage(bmp);

            FillBackground(graphics, bmp, backgroundBrush);
            WriteWordsAtBitmap(wordsAndRectangles, graphics, wordBrush, bmp, font);

            graphics.Flush();
            return bmp;
        }

        private static void WriteWordsAtBitmap(IEnumerable<(string word, Rectangle rectangle)> pairs,
            Graphics graphics,
            Brush brush,
            Bitmap bmp,
            Font font)
        {
            var random = new Random();
            foreach (var (word, rect) in pairs)
            {
                rect.Offset(+bmp.Width / 2, +bmp.Height / 2);
                graphics.DrawString(word, font, brush, rect.X, rect.Y);
            }
        }

        public static Color GetRandomColor(Random random)
        {
            return Color.FromArgb(
                random.Next(255),
                random.Next(255),
                random.Next(255));
        }
    }
}