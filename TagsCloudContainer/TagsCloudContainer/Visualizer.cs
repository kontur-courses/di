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
            return GetCloudVisualization(words, tagsColors, backgroundColor,
                minTagSize, maxTagSize, layouter, reductionCoefficient,
                minFontSize, fontFamily, new List<Brush>() {textBrush});
        }
        
        public static Bitmap GetCloudVisualization(List<string> words, List<Color> tagsColors, Color backgroundColor,
            Size minTagSize, Size maxTagSize, CircularCloudLayouter layouter, double reductionCoefficient,
            float minFontSize, FontFamily fontFamily, List<Brush> brushes)
        {
            if (words == null)
            {
                throw new ArgumentException("Words can't be null");
            }

            if (tagsColors == null)
            {
                throw new ArgumentException("Tags colors can't be null");
            }

            GenerateRectangles(words, minTagSize, maxTagSize, layouter, reductionCoefficient);
            var bitmapWithRectangles = CloudVisualizer.Draw(layouter, tagsColors, backgroundColor);
            AddWordsToImage(bitmapWithRectangles, layouter.Rectangles, words, minFontSize, fontFamily, brushes);
            return bitmapWithRectangles;
        }

        private static void GenerateRectangles(List<string> words, Size minSize, Size maxSize,
            CircularCloudLayouter layouter, double reductionCoefficient)
        {
            if (maxSize.Height < minSize.Height || maxSize.Width < minSize.Width)
            {
                throw new ArgumentException("Min tag size must be less or equal than max tag size");
            }

            if (reductionCoefficient <= 0 || reductionCoefficient >= 1)
            {
                throw new ArgumentException("Reduction coefficient must be between 0 and 1");
            }

            if (layouter == null)
            {
                throw new ArgumentException("Layouter can't be null");
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
            float minFontSize, FontFamily fontFamily, List<Brush> brushes)
        {
            if (minFontSize <= 0)
            {
                throw new ArgumentException("Font size can't be zero or negative");
            }

            if (fontFamily == null)
            {
                throw new ArgumentException("Font family can't be null");
            }

            if (brushes == null)
            {
                throw new ArgumentException("Brush can't be null");
            }

            var rnd = new Random();
            var graphics = Graphics.FromImage(bitmap);
            for (var i = 0; i < words.Count && i < rectangles.Count; i++)
            {
                var brushIndex = brushes.Count == Math.Min(words.Count, rectangles.Count)
                    ? i : rnd.Next(0, brushes.Count);
                var fontSize = GetFontSize(rectangles[i], words[i], minFontSize);
                graphics.DrawString(words[i], new Font(fontFamily, fontSize), brushes[brushIndex], rectangles[i]);
            }
        }

        private static float GetFontSize(Rectangle rectangle, string word, float minFontSize)
        {
            var size = TextRenderer.MeasureText(word, new Font(FontFamily.GenericSansSerif, minFontSize));
            if (size.Width > rectangle.Width || size.Height > rectangle.Height)
            {
                throw new ArgumentException("Word is too long for tag cloud");
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