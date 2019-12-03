using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TagsCloudVisualization.Settings;
using TagsCloudVisualization.TagCloudLayouters;


namespace TagsCloudVisualization.TagsCloudVisualization
{
    class WordsCloudVisualization : ITagsCloudVisualization<Rectangle>
    {
        private readonly Dictionary<string, int> words;
        private readonly ImageSettings imageSettings;
        private readonly CircularCloudLayouter circularCloudLayouter;

        public WordsCloudVisualization(Dictionary<string, int> words, ImageSettings imageSettings, CircularCloudLayouter circularCloudLayouter)
        {
            this.words = words;
            this.imageSettings = imageSettings;
            this.circularCloudLayouter = circularCloudLayouter;
        }

        public void Draw()
        {
            var image = new Bitmap(imageSettings.ImageSize.Width, imageSettings.ImageSize.Height);
            var stringFormat = new StringFormat {Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center};
            using (var drawPlace = Graphics.FromImage(image))
            {
                foreach (var wordInfo in words)
                {
                    var font = new Font("Consolas", imageSettings.MinimalTextSize * wordInfo.Value);
                    var rectangleSize = TextRenderer.MeasureText(wordInfo.Key, font);
                    var rectangle = circularCloudLayouter.PutNextRectangle(rectangleSize);
                    drawPlace.DrawString(wordInfo.Key, font, new SolidBrush(Color.Black), rectangle, stringFormat);
                }
            }
            image.Save(imageSettings.ImageName + imageSettings.ImageExtention);
        }
    }
}
