using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.CloudPainters;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.WordSizing;

namespace TagsCloudVisualization.Visualization
{
    public class TagCloudSizedVisualizer : ICloudVisualizer<Tuple<SizedWord, Rectangle>>
    {
        private readonly VisualisingOptions visualisingOptions;

        public TagCloudSizedVisualizer(VisualisingOptions visualisingOptions)
        {
            this.visualisingOptions = visualisingOptions;
        }

        public Bitmap GetVisualization(IEnumerable<string> words, ILayouter layouter,
            ICloudPainter<Tuple<SizedWord, Rectangle>> cloudPainter)
        {
            var sizedWords = new FrequencyWordSizer().GetSizedWords(words, (int)visualisingOptions.Font.Size, (int)visualisingOptions.Font.Size/2);
            var rectangles = GetRectanglesForWords(sizedWords, layouter);
            return cloudPainter.GetImage(sizedWords.Zip(rectangles, Tuple.Create), visualisingOptions);
        }

        private IEnumerable<Rectangle> GetRectanglesForWords(IEnumerable<SizedWord> words, ILayouter layouter)
        {
            return words.Select(word =>
                layouter.PutNextRectangle(GetWordSize(word.Value,
                    new Font(visualisingOptions.Font.FontFamily, word.Size), visualisingOptions.ImageSize)));
        }

        private Size GetWordSize(string word, Font font, Size pictureSize)
        {
            //return TextRenderer.MeasureText(word, font);
            var bitmap = new Bitmap(pictureSize.Width, pictureSize.Height);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                return graphics.MeasureString(word, font).ToSize();
            }
        }
    }
}