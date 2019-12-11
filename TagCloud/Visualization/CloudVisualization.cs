using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.CloudLayouter;
using TagCloud.TextColoration;
using TagCloud.TextConversion;

namespace TagCloud.Visualization
{
    public class CloudVisualization
    {
        private readonly ImageSettings imageSettings;
        private readonly ICloudLayouter layouter;
        private readonly ViewSettings viewSettings;
        private readonly ITextColoration textColoration;
        private readonly FrequencyDictionaryMaker frequencyDictionaryMaker;
        private readonly TextConverter textConverter;
        private Dictionary<string, int> words;
        private readonly Random random = new Random();

        public CloudVisualization(ImageSettings imageSettings, ICloudLayouter layouter,
            ViewSettings viewSettings, ITextColoration textColoration,
            FrequencyDictionaryMaker frequencyDictionaryMaker, TextConverter textConverter)
        {
            this.imageSettings = imageSettings;
            this.layouter = layouter;
            this.viewSettings = viewSettings;
            this.textColoration = textColoration;
            this.frequencyDictionaryMaker = frequencyDictionaryMaker;
            this.textConverter = textConverter;
            ResetWordsFrequenciesDictionary();
        }

        public Bitmap Visualize()
        {
            var bitmap = new Bitmap(imageSettings.ImageSize.Width, imageSettings.ImageSize.Height);
            using (var graphics =
                Graphics.FromImage(bitmap))
            {
                layouter.ResetLayouter();
                graphics.FillRectangle(new SolidBrush(viewSettings.BackgroundColor), 0, 0,
                    imageSettings.ImageSize.Width, imageSettings.ImageSize.Height);
                var centerPoint = GetCenterPoint();
                graphics.TranslateTransform(centerPoint.X, centerPoint.Y);
                foreach (var word in words.OrderByDescending(w => w.Value).Take(viewSettings.WordsCount))
                    PaintRectangleOnCanvas(word.Key, word.Value, graphics);
            }

            return bitmap;
        }

        private void PaintRectangleOnCanvas(string word, int frequency, Graphics graphics)
        {
            var newRectangleSize = SetNewRectangleSize(frequency, word.Length);
            var color = viewSettings.Colors.ElementAt(random.Next(0, viewSettings.Colors.Count));

            var rectangle = layouter.TryPutNextRectangle(newRectangleSize, out var outRectangle)
                ? outRectangle
                : Rectangle.Empty;

            if (viewSettings.EnableWordRectangles)
                graphics.FillRectangle(color, rectangle);
            if (!rectangle.IsEmpty)
                graphics.DrawString(word, new Font(viewSettings.FontName, GetFontSize(word, newRectangleSize.Width)),
                    textColoration.GetTextColor(word, frequency), rectangle);
        }

        private Point GetCenterPoint()
        {
            return new Point(imageSettings.ImageSize.Width / 2, imageSettings.ImageSize.Height / 2);
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

        public void ResetWordsFrequenciesDictionary()
        {
            words = frequencyDictionaryMaker.MakeFrequencyDictionary(textConverter.ConvertWords());
        }
    }
}