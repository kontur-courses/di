using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TagsCloudContainer.TagsCloudVisualization;

namespace TagsCloudContainer
{
    public static class Visualizer
    {
        public static Bitmap GetCloudVisualization(List<string> words, List<Color> tagsColors, Color backgroundColor,
            Size minTagSize, Size maxTagSize, CircularCloudLayouter layouter, double reductionCoefficient,
            float minFontSize, FontFamily fontFamily, Brush textBrush)
        {
            if (words == null || tagsColors == null || maxTagSize.Height < minTagSize.Height ||
                maxTagSize.Width < minTagSize.Width || layouter == null || minFontSize <= 0 || fontFamily == null ||
                textBrush == null)
            {
                throw new ArgumentException();
            }

            GenerateRectangles(words, minTagSize, maxTagSize, layouter, reductionCoefficient);
            var bitmapWithRectangles = CloudVisualizer.Draw(layouter, tagsColors, backgroundColor);
            AddWordsToImage(bitmapWithRectangles, layouter.Rectangles, words, minFontSize, fontFamily, textBrush);
            return bitmapWithRectangles;
        }

        private static void GenerateRectangles(List<string> words, Size minSize, Size maxSize,
            CircularCloudLayouter layouter, double reductionCoefficient)
        {
            if (reductionCoefficient <= 0 || reductionCoefficient >= 1)
            {
                throw new ArgumentException();
            }

            var currentSize = maxSize;
            foreach (var _ in words)
            {
                currentSize.Width = currentSize.Width < minSize.Width ? minSize.Width : currentSize.Width;
                currentSize.Height = currentSize.Height < minSize.Height ? minSize.Height : currentSize.Height;
                layouter.PutNextRectangle(currentSize);
                currentSize.Height = (int) (currentSize.Height * reductionCoefficient);
                currentSize.Width = (int) (currentSize.Width * reductionCoefficient);
            }
        }

        private static void AddWordsToImage(Bitmap bitmap, List<Rectangle> rectangles, List<string> words,
            float minFontSize, FontFamily fontFamily, Brush textBrush)
        {
            var graphics = Graphics.FromImage(bitmap);
            for (var i = 0; i < words.Count && i < rectangles.Count; i++)
            {
                var fontSize = GetFontSize(rectangles[i], words[i], minFontSize);
                graphics.DrawString(words[i], new Font(fontFamily, fontSize), textBrush, rectangles[i]);
            }
        }

        private static float GetFontSize(Rectangle rectangle, string word, float minFontSize)
        {
            var size = TextRenderer.MeasureText(word, new Font(FontFamily.GenericSansSerif, minFontSize));
            if (size.Width > rectangle.Width || size.Height > rectangle.Height)
            {
                throw new ArgumentException("word is too long for tag cloud");
            }

            var currentFontSize = minFontSize;
            while (size.Width <= rectangle.Width && size.Height <= rectangle.Height)
            {
                currentFontSize++;
                size = TextRenderer.MeasureText(word, new Font(FontFamily.GenericSansSerif, currentFontSize));
            }

            return currentFontSize - 1;
        }
    }
}