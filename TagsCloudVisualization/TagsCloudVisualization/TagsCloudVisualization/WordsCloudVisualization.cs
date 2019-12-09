using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TagsCloudVisualization.Settings;
using TagsCloudVisualization.TagCloudLayouters;
using System.Linq;


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
            var drawingInfo = new Dictionary<string, (RectangleF rectangle, Font font)>();
            foreach(var wordInfo in words)
            {
                var font = new Font(imageSettings.Font, imageSettings.MinimalTextSize * wordInfo.Value);
                var rectangleSize = TextRenderer.MeasureText(wordInfo.Key, font);
                var rectangle = circularCloudLayouter.PutNextRectangle(rectangleSize);
                drawingInfo.Add(wordInfo.Key, (rectangle, font));
            }

            var width = imageSettings.ImageSize.Width == 0 ?
                drawingInfo.Max(elem => elem.Value.rectangle.X) : imageSettings.ImageSize.Width;
            var height = imageSettings.ImageSize.Height == 0 ?
                drawingInfo.Max(elem => elem.Value.rectangle.Y) : imageSettings.ImageSize.Height;

            using (var image = new Bitmap((int)width, (int)height))
                imageSettings.TextRenderer.PrintWords(image, drawingInfo, imageSettings);
        }
    }
}
