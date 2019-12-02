using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using TagCloud.CloudLayouter;

namespace TagCloud.Visualization
{
    public class TagCloudVisualization
    {
        private readonly string name;
        private Graphics graphics;
        private Bitmap image;
        private readonly Random random = new Random();
        private readonly CircularCloudLayouter layouter;
        private readonly ImageSettings imageSettings;
        private readonly RectangleSettings rectangleSettings;
        private readonly VisualizationSettings visualizationSettings;
        private readonly Dictionary<string, int> words;

        private readonly Brush[] colors =
        {
            Brushes.Aqua, Brushes.Lime, Brushes.Blue, Brushes.Brown, Brushes.Chartreuse,
            Brushes.Chocolate, Brushes.Coral, Brushes.Crimson, Brushes.MediumSlateBlue,
            Brushes.Gold, Brushes.Green, Brushes.Fuchsia, Brushes.BlueViolet
        };

        public TagCloudVisualization(CircularCloudLayouter layouter,
            VisualizationSettings visualizationSettings,
            TextPreparer textPreparer, ImageSettings imageSettings,
            RectangleSettings rectangleSettings)
        {
            words = textPreparer.GetParsedTextDictionary();
            this.layouter = layouter;
            this.imageSettings = imageSettings;
            this.visualizationSettings = visualizationSettings;
            this.rectangleSettings = rectangleSettings;
            name = visualizationSettings.FileName;
            RecreateImage();
            SetGraphics();
        }

        private void RecreateImage()
        {
            image = new Bitmap(imageSettings.Width, imageSettings.Height);
        }

        private void SetGraphics()
        {
            graphics = Graphics.FromImage(image);
            graphics.TranslateTransform(image.Width / 2, image.Height / 2);
        }

        private void PaintRectangleOnCanvas(string word, int frequency)
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
            rectangleSettings.Height = rectangleSettings.Width / wordLength * 2;
        }

        public void SaveTagCloud()
        {
            image.Save(name + ".png", ImageFormat.Png);
        }

        public void MakeTagCloud()
        {
            RecreateImage();
            SetGraphics();
            layouter.RefreshLayouter();
            foreach (var word in words.OrderByDescending(w => w.Value).Take(10))
                PaintRectangleOnCanvas(word.Key, word.Value);
        }
    }
}