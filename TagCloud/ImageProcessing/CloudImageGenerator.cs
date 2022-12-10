using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.CloudLayouter;

namespace TagCloud.ImageProcessing
{
    public class CloudImageGenerator
    {
        private readonly ICloudLayouter layouter;

        private readonly IImageSettings imageSettings;

        private Graphics graphics;

        private readonly Color rectangleBorderColor;
        public CloudImageGenerator(ICloudLayouter layouter, Color rectangleBorderColor)
        {
            this.layouter = layouter;
            this.rectangleBorderColor = rectangleBorderColor;
        }

        public CloudImageGenerator(ICloudLayouter layouter, IImageSettings imageSettings)
        {
            this.layouter = layouter;
            this.imageSettings = imageSettings;
        }

        public Bitmap GenerateBitmap(IEnumerable<Rectangle> layout)
        {
            var layoutArray = layout.ToArray();

            var imageSize = GetImageSize(layoutArray);

            var bitmap = new Bitmap(imageSize.Width, imageSize.Height);

            var graphics = Graphics.FromImage(bitmap);

            graphics.TranslateTransform(imageSize.Width / 2f - layouter.CloudCenter.X, imageSize.Height / 2f - layouter.CloudCenter.Y);

            var pen = new Pen(rectangleBorderColor, 1);

            graphics.DrawRectangles(pen, layoutArray.ToArray());

            graphics.Dispose();

            return bitmap;
        }

        public Bitmap GenerateBitmap(IReadOnlyDictionary<string, double> wordsFrequencies)
        {
            var width = imageSettings.Size.Width;

            var height = imageSettings.Size.Height;

            var bitmap = new Bitmap(width, height);

            graphics = Graphics.FromImage(bitmap);

            graphics.FillRectangle(new SolidBrush(imageSettings.BackgroundColor), 0, 0, width, height);

            graphics.TranslateTransform(width / 2f - layouter.CloudCenter.X, height / 2f - layouter.CloudCenter.Y);

            DrawWords(wordsFrequencies);

            graphics.Dispose();

            return bitmap;
        }

        private void DrawWords(IReadOnlyDictionary<string, double> wordsFrequencies)
        {
            double minFrequency = wordsFrequencies.Min(x => x.Value);

            double maxFrequency = wordsFrequencies.Max(x => x.Value);

            foreach (var wordFreq in wordsFrequencies)
            {
                var font = GetWordFontByFrequency(imageSettings.MinFontSize, imageSettings.MaxFontSize, minFrequency, maxFrequency, wordFreq.Value);

                var rectangleSize = GetWordLayoutRectangleSize(wordFreq.Key, font);

                var rectangle = layouter.PutNextRectangle(rectangleSize);

                var stringFormat = new StringFormat() 
                {
                   Alignment = StringAlignment.Center,
                   LineAlignment = StringAlignment.Center,
                };

                //graphics.DrawRectangle(new Pen(Color.Black, 1), layoutRectangle);

                graphics.DrawString(wordFreq.Key, font, new SolidBrush(imageSettings.WordColoring.GetColor(wordFreq.Value)), rectangle, stringFormat);
            }
        }

        private Font GetWordFontByFrequency(int minFontSize, int maxFontSize, double minFrequency, double maxFrequency, double wordFrequency)
        {
            var fontSize = (int)(minFontSize + (maxFontSize - minFontSize) * (wordFrequency - minFrequency) / (maxFrequency - minFrequency));

            return new Font(imageSettings.FontFamily, fontSize);
        }

        private Size GetWordLayoutRectangleSize(string word, Font font)
        {
            var wordSize = graphics.MeasureString(word, font);

            var width = (int)Math.Ceiling(wordSize.Width);

            var height = (int)Math.Ceiling(wordSize.Height);

            return new Size(width, height);
        }

        private Size GetImageSize(IEnumerable<Rectangle> layout)
        {
            var minTop = int.MaxValue;
            var maxBottom = -int.MaxValue;
            var minLeft = int.MaxValue;
            var maxRight = -int.MaxValue;

            foreach (var rectangle in layout)
            {
                if (rectangle.Top < minTop)
                    minTop = rectangle.Top;

                if (rectangle.Bottom > maxBottom)
                    maxBottom = rectangle.Bottom;

                if (rectangle.Left < minLeft)
                    minLeft = rectangle.Left;

                if (rectangle.Right > maxRight)
                    maxRight = rectangle.Right;
            }

            var width = 2 * Math.Max(Math.Abs(minLeft), Math.Abs(maxRight)) + 5;

            var height = 2 * Math.Max(Math.Abs(maxBottom), Math.Abs(minTop)) + 5;

            return new Size(width, height);
        }
    }
}
