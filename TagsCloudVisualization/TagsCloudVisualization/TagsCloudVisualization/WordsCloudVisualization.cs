using System.Collections.Generic;
using System.Drawing;
using System.Linq;


namespace TagsCloudVisualization.TagsCloudVisualization
{
    class WordsCloudVisualization : ITagsCloudVisualization<Rectangle>
    {
        private Dictionary<string, Font> words;
        public WordsCloudVisualization(Dictionary<string, Font> words)
        {
            this.words = words;
        }

        public Bitmap Draw(IEnumerable<Rectangle> figuresToDraw, int imageWidth, int imageHeight)
        {
            var image = new Bitmap(imageWidth, imageHeight);
            var rectangles = figuresToDraw.ToList();
            var stringFormat = new StringFormat {Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center};
            using (var drawPlace = Graphics.FromImage(image))
            {
                foreach (var (rectangle, wordInfo) in rectangles.Zip(words, (place, wordInfo) => (place, wordInfo)))
                    drawPlace.DrawString(wordInfo.Key, wordInfo.Value, new SolidBrush(Color.Black), rectangle, stringFormat);
            }
            return image;
        }
    }
}
