using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.CloudLayouter;
using TagCloud.WordColoring;

namespace TagCloud.ImageProcessing
{
    public class CloudImageGenerator : ICloudImageGenerator
    {
        private readonly ICloudLayouter layouter;

        private readonly IWordColoring wordColoring;

        private readonly IImageSettings imageSettings;

        private Graphics graphics;

        public CloudImageGenerator(ICloudLayouter layouter, IImageSettings imageSettings, IWordColoring wordColoring)
        {                    
            this.layouter = layouter;
            this.imageSettings = imageSettings;
            this.wordColoring = wordColoring;
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

                graphics.DrawString(wordFreq.Key, font, new SolidBrush(wordColoring.GetColor(wordFreq.Value)), rectangle, stringFormat);
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
    }
}
