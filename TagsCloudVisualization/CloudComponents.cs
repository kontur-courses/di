using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.Visualization;
using TagsCloudVisualization.WordSizing;

namespace TagsCloudVisualization
{
    public class CloudComponents
    {
        public ILayouter Layouter { get; set; }
        public VisualisingOptions VisualisingOptions { get; set; }
        public IEnumerable<string> Words { get; set; }
        public IEnumerable<Rectangle> Rectangles { get; set; }

        public IEnumerable<SizedWord> GetSizedWords(int minSize = 10, int step = 5)
        {
            return new FrequencyWordSizer().GetSizedWords(Words, minSize, step);
        }

        public IEnumerable<Rectangle> GetRectanglesForSizedWords(IEnumerable<SizedWord> sizedWords)
        {
            return sizedWords.Select(word =>
                Layouter.PutNextRectangle(GetWordSize(word.Value,
                    new Font(VisualisingOptions.Font.FontFamily, word.Size), VisualisingOptions.ImageSize)));
        }

        public IEnumerable<Rectangle> GetRectanglesForWords()
        {
            return Words.Select(word =>
                Layouter.PutNextRectangle(GetWordSize(word, VisualisingOptions.Font, VisualisingOptions.ImageSize)));
        }

        private Size GetWordSize(string word, Font font, Size pictureSize)
        {
            var bitmap = new Bitmap(pictureSize.Width, pictureSize.Height);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                return graphics.MeasureString(word, font).ToSize();
            }
        }
    }
}