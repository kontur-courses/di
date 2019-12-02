using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud;
using TagCloud.CloudLayouter;
using TagCloud.Visualization;
using TagCloudForm.Holder;

namespace TagCloudForm
{
    public class CloudPainter
    {
        private readonly IImageHolder imageHolder;
        private readonly RectangleSettings rectangleSettings;
        private readonly CircularCloudLayouter layouter;
        private readonly VisualizationSettings visualizationSettings;
        private readonly ImageSettings imageSettings;
        private readonly Dictionary<string, int> words;
        private readonly Random random = new Random();

        private readonly Brush[] colors =
        {
            Brushes.Aqua, Brushes.Lime, Brushes.Blue, Brushes.Brown, Brushes.Chartreuse,
            Brushes.Chocolate, Brushes.Coral, Brushes.Crimson, Brushes.MediumSlateBlue,
            Brushes.Gold, Brushes.Green, Brushes.Fuchsia, Brushes.BlueViolet
        };

        public CloudPainter(IImageHolder imageHolder, RectangleSettings rectangleSettings,
            CircularCloudLayouter layouter, VisualizationSettings visualizationSettings, TextPreparer textPreparer,
            ImageSettings imageSettings)
        {
            this.imageHolder = imageHolder;
            this.rectangleSettings = rectangleSettings;
            this.layouter = layouter;
            this.visualizationSettings = visualizationSettings;
            this.imageSettings = imageSettings;
            words = textPreparer.GetParsedTextDictionary();
        }

        public void Paint()
        {
            imageHolder.RecreateImage(imageSettings);
            using (var graphics = imageHolder.StartDrawing())
            {
                layouter.RefreshLayouter();
                graphics.TranslateTransform(imageSettings.Width / 2, imageSettings.Height / 2);
                foreach (var word in words.OrderByDescending(w => w.Value).Take(100))
                    PaintRectangleOnCanvas(word.Key, word.Value, graphics);
            }

            imageHolder.UpdateUi();
        }

        private void PaintRectangleOnCanvas(string word, int frequency, Graphics graphics)
        {
            SetNewRectangleSize(frequency, word.Length);
            SetNewFont(word.Length);
            var rectangle = layouter.PutNextRectangle(
                rectangleSettings.RectangleSize, word, frequency);
            graphics.FillRectangle(colors[random.Next(0, colors.Length)], rectangle);
            graphics.DrawString(word, visualizationSettings.VisualizationFont,
                Brushes.Black, rectangle);
        }

        private void SetNewFont(int wordLength)
        {
            visualizationSettings.FontName = "Arial";
            visualizationSettings.FontSize = rectangleSettings.Width / wordLength;
        }

        private void SetNewRectangleSize(int frequency, int wordLength)
        {
            rectangleSettings.Width = (int) ((Math.Log(frequency) + 1) * 10 * wordLength);
            rectangleSettings.Height = 2 * rectangleSettings.Width / wordLength;
        }
    }
}