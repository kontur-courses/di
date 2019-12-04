using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud;
using TagCloud.CloudLayouter;
using TagCloud.TextFilter;
using TagCloud.TextProvider;
using TagCloud.Visualization;
using TagCloudForm.Holder;

namespace TagCloudForm
{
    public class CloudPainter
    {
        private readonly IImageHolder imageHolder;
        private readonly RectangleSettings rectangleSettings;
        private readonly ICloudLayouter layouter;
        private readonly ViewSettings viewSettings;
        private readonly ImageSettings imageSettings;
        private readonly Dictionary<string, int> words;
        private readonly Random random = new Random();

        public CloudPainter(IImageHolder imageHolder, RectangleSettings rectangleSettings,
            ICloudLayouter layouter, ViewSettings viewSettings, TextFilter textFilter,
            ImageSettings imageSettings, ITextProvider textProvider)
        {
            this.imageHolder = imageHolder;
            this.rectangleSettings = rectangleSettings;
            this.layouter = layouter;
            this.viewSettings = viewSettings;
            this.imageSettings = imageSettings;
            words = textFilter.FilterWords(textProvider.GetParsedText());
        }

        public void Paint()
        {
            imageHolder.RecreateImage(imageSettings);
            using (var graphics = imageHolder.StartDrawing())
            {
                layouter.RefreshLayouter();
                graphics.FillRectangle(new SolidBrush(viewSettings.BackgroundColor), 0, 0, imageSettings.Width,
                    imageSettings.Height);
                var centerPoint = GetCenterPoint();
                graphics.TranslateTransform(centerPoint.X, centerPoint.Y);
                foreach (var word in words.OrderByDescending(w => w.Value).Take(viewSettings.WordsCount))
                    PaintRectangleOnCanvas(word.Key, word.Value, graphics);
            }

            imageHolder.UpdateUi();
        }

        private Point GetCenterPoint()
        {
            return new Point(imageSettings.Width / 2, imageSettings.Height / 2);
        }

        private void PaintRectangleOnCanvas(string word, int frequency, Graphics graphics)
        {
            SetNewRectangleSize(frequency, word.Length);
            var rectangle = layouter.PutNextRectangle(
                rectangleSettings.RectangleSize);
            if (viewSettings.EnableWordRectangles)
                graphics.FillRectangle(viewSettings.colors.ElementAt(random.Next(0, viewSettings.colors.Count)),
                    rectangle);
            graphics.DrawString(word, new Font(viewSettings.FontName, GetFontSize(word)),
                new SolidBrush(viewSettings.TextColor), rectangle);
        }

        private float GetFontSize(string word)
        {
            return rectangleSettings.Width / word.Length;
        }

        private void SetNewRectangleSize(int frequency, int wordLength)
        {
            rectangleSettings.Width = (int) ((Math.Log(frequency) + 1) * 10 * wordLength);
            rectangleSettings.Height = 2 * rectangleSettings.Width / wordLength;
        }
    }
}