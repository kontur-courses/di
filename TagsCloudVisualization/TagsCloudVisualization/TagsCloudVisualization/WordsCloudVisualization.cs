using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TagsCloudVisualization.Settings;
using TagsCloudVisualization.TagCloudLayouters;


namespace TagsCloudVisualization.TagsCloudVisualization
{
    public class WordsCloudVisualization : ITagsCloudVisualization<Rectangle>
    {
        private readonly ImageSettings imageSettings;
        private readonly ILayouter circularCloudLayouter;

        public WordsCloudVisualization(ImageSettings imageSettings, ILayouter circularCloudLayouter)
        {
            this.imageSettings = imageSettings;
            this.circularCloudLayouter = circularCloudLayouter;
        }

        public void Draw(Dictionary<string, int> words)
        {
            var stringFormat = new StringFormat
            {
                Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center
            };

            var colorSelector = new Random();
            using (var image = new Bitmap(imageSettings.ImageSize.Width, imageSettings.ImageSize.Height))
            using (var drawPlace = Graphics.FromImage(image))
            {
                foreach (var wordInfo in words)
                {
                    using (var font = new Font(imageSettings.Font, imageSettings.MinimalTextSize * wordInfo.Value))
                    {
                        var rectangleSize = TextRenderer.MeasureText(wordInfo.Key, font);
                        var rectangle = circularCloudLayouter.PutNextRectangle(rectangleSize);
                        var color = imageSettings.Colors[colorSelector.Next(imageSettings.Colors.Count)];
                        drawPlace.DrawString(wordInfo.Key, font, new SolidBrush(color), rectangle, stringFormat);
                    }
                }

                image.Save(imageSettings.ImageName + imageSettings.ImageExtention);
            }
        }
    }
}
