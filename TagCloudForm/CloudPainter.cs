using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.CloudLayouter;
using TagCloud.TextFilter;
using TagCloud.Visualization;
using TagCloudForm.Holder;

namespace TagCloudForm
{
    public class CloudPainter
    {
        private readonly IImageHolder imageHolder;
        private readonly ICloudLayouter layouter;
        private readonly ViewSettings viewSettings;
        private readonly ImageSettings imageSettings;
        private readonly FrequencyDictionaryMaker frequencyDictionaryMaker;
        private readonly Random random = new Random();
        private Dictionary<string, int> words;

        public CloudPainter(IImageHolder imageHolder,
            ICloudLayouter layouter, ViewSettings viewSettings,
            ImageSettings imageSettings, FrequencyDictionaryMaker frequencyDictionaryMaker)
        {
            this.imageHolder = imageHolder;
            this.layouter = layouter;
            this.viewSettings = viewSettings;
            this.imageSettings = imageSettings;
            this.frequencyDictionaryMaker = frequencyDictionaryMaker;
            ResetWordsFrequenciesDictionary();
        }

        public void ResetWordsFrequenciesDictionary()
        {
            words = frequencyDictionaryMaker.MakeFrequencyDictionary();
        }

        public void Paint()
        {
            imageHolder.RecreateImage(imageSettings);
            using (var graphics = imageHolder.StartDrawing())
            {
                layouter.ResetLayouter();
                graphics.FillRectangle(new SolidBrush(viewSettings.BackgroundColor), 0, 0,
                    imageSettings.ImageSize.Width, imageSettings.ImageSize.Height);
                var centerPoint = GetCenterPoint();
                graphics.TranslateTransform(centerPoint.X, centerPoint.Y);
                foreach (var word in words.OrderByDescending(w => w.Value).Take(viewSettings.WordsCount))
                    PaintRectangleOnCanvas(word.Key, word.Value, graphics);
            }

            imageHolder.UpdateUi();
        }

        private Point GetCenterPoint()
        {
            return new Point(imageSettings.ImageSize.Width / 2, imageSettings.ImageSize.Height / 2);
        }

        private void PaintRectangleOnCanvas(string word, int frequency, Graphics graphics)
        {
            var newRectangleSize = SetNewRectangleSize(frequency, word.Length);
            var color = viewSettings.colors.ElementAt(random.Next(0, viewSettings.colors.Count));

            var rectangle = layouter.TryPutNextRectangle(newRectangleSize, out var outRectangle)
                ? outRectangle
                : Rectangle.Empty;

            if (viewSettings.EnableWordRectangles)
                graphics.FillRectangle(color, rectangle);
            if (!rectangle.IsEmpty)
                graphics.DrawString(word, new Font(viewSettings.FontName, GetFontSize(word, newRectangleSize.Width)),
                    new SolidBrush(viewSettings.TextColor), rectangle);
        }

        private float GetFontSize(string word, int width)
        {
            return width / word.Length;
        }

        private Size SetNewRectangleSize(int frequency, int wordLength)
        {
            var width = (int) ((Math.Log(frequency) + 1) * 10 * wordLength);
            var height = 2 * width / wordLength;
            return new Size(width, height);
        }
    }
}