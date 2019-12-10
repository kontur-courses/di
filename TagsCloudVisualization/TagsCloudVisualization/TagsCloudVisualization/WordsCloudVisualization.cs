using System.Collections.Generic;
using System.Windows.Forms;
using TagsCloudVisualization.Settings;
using TagsCloudVisualization.TagCloudLayouters;
using System.Linq;
using System.Drawing;

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
            var wordsRectangles = new Dictionary<string, Rectangle>();
            foreach(var wordInfo in words)
            {
                var size = imageSettings.TextRenderer.GetRectangleSize(imageSettings, wordInfo);
                var rectangle = circularCloudLayouter.PutNextRectangle(size);
                wordsRectangles.Add(wordInfo.Key, rectangle);
            }

            var width = imageSettings.ImageSize.Width == 0 ?
                wordsRectangles.Max(elem => elem.Value.X) : imageSettings.ImageSize.Width;
            var height = imageSettings.ImageSize.Height == 0 ?
                wordsRectangles.Max(elem => elem.Value.Y) : imageSettings.ImageSize.Height;

            imageSettings.TextRenderer.PrintWords((int)width, (int)height, wordsRectangles, imageSettings);
        }
    }
}
