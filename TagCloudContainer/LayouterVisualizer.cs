using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace TagCloudContainer
{
    public static class LayouterVisualizer
    {
        public static void CreateCloudWithWordsFromFile(string wordSourceFile, int baseFontSize, string outputFile)
        {
            var words = File.ReadLines(wordSourceFile);
            var bitmap = CreateBitmapOfCloudWithWords(words, baseFontSize);
            bitmap.Save(outputFile);
        }

        public static void SaveBitmapWithRectanglesFromLayout(CircularCloudLayouter layouter, string outputFile)
        {
            var bmp = CreateSizedBitmapForLayouter(layouter);
            var graphics = Graphics.FromImage(bmp);
            FillBackground(graphics, bmp, Color.White);

            DrawRectanglesAtBitmap(bmp, layouter, graphics, Color.Blue);

            graphics.Flush();
            bmp.Save(outputFile);
        }


        private static Bitmap CreateSizedBitmapForLayouter(CircularCloudLayouter layouter)
        {
            int maxX = layouter.Layout.Select(r => r.Right).Max();
            int minX = layouter.Layout.Select(r => r.Left).Min();
            int maxY = layouter.Layout.Select(r => r.Bottom).Max();
            int minY = layouter.Layout.Select(r => r.Top).Min();

            int bmpWidth = maxX - minX;
            int bmpHeight = maxY - minY;

            return new Bitmap(bmpWidth, bmpHeight);
        }

        private static void FillBackground(Graphics graphics, Bitmap bmp, Color color)
        {
            graphics.FillRegion(new SolidBrush(color), new Region(new Rectangle(0, 0, bmp.Width, bmp.Width)));
        }

        private static void DrawRectanglesAtBitmap(
            Bitmap bmp,
            CircularCloudLayouter layouter,
            Graphics graphics,
            Color rectColor)
        {
            var pen = new Pen(new SolidBrush(rectColor));
            foreach (var rect in layouter.Layout)
            {
                rect.Offset(new Point(bmp.Width / 2, bmp.Height / 2));
                graphics.DrawRectangle(pen, rect);
            }
        }


        private static Bitmap CreateBitmapOfCloudWithWords(IEnumerable<string> words, int baseFontSize)
        {
            var layouter = new CircularCloudLayouter();
            var pairs = AddRectanglesToLayouter(layouter, words, baseFontSize);

            var bmp = CreateSizedBitmapForLayouter(layouter);
            var graphics = Graphics.FromImage(bmp);

            FillBackground(graphics, bmp, Color.White);
            WriteWordsAtBitmap(pairs, layouter, bmp, graphics);

            graphics.Flush();
            return bmp;
        }

        private static void WriteWordsAtBitmap(List<Tuple<string, Rectangle>> pairs,
            CircularCloudLayouter layouter,
            Bitmap bmp,
            Graphics graphics)
        {
            var random = new Random();
            foreach (var (word, rect) in pairs)
            {
                rect.Offset(+bmp.Width / 2, +bmp.Height / 2);
                var font = new Font("Helvetica", (float) rect.Width * 3 / word.Length / 2);
                graphics.DrawString(word, font, new SolidBrush(GetRandomColor(random)),
                    rect.X, rect.Y);
            }
        }

        private static Color GetRandomColor(Random random)
        {
            return Color.FromArgb(
                random.Next(255),
                random.Next(255),
                random.Next(255));
        }

        private static List<Tuple<string, Rectangle>> AddRectanglesToLayouter(
            CircularCloudLayouter layouter,
            IEnumerable<string> words,
            int baseFontSize)
        {
            var graphicsBase = Graphics.FromImage(new Bitmap(1, 1));
            var random = new Random();
            var fontBase = new Font("Helvetica", baseFontSize);
            return words.Select(s =>
                {
                    var variation = random.Next(16);
                    return Tuple.Create(s, graphicsBase.MeasureString(s, fontBase) * variation);
                }).OrderBy(r => Tuple.Create(r.Item1, -r.Item2.Height))
                .Select(s => Tuple.Create(s.Item1, layouter.PutNextRectangle(s.Item2.ToSize())))
                .ToList();
        }
    }
}