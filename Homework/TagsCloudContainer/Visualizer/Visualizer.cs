using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Layouter;

namespace TagsCloudContainer.Visualizer
{
    public class Visualizer : IVisualizer
    {
        private readonly IVisualizerSettings settings;
        private readonly ICloudLayouter layouter;

        public Visualizer(IVisualizerSettings settings, ICloudLayouter layouter)
        {
            this.settings = settings;
            this.layouter = layouter;
        }

        public Bitmap Visualize(Dictionary<string, int> freqDict)
        {
            var wordsOrederedByFreq = freqDict
                .OrderByDescending(w => w.Value);
            var layoutRectangles = GetLayoutRectangles(wordsOrederedByFreq);
            var imageSize = settings.ImageSize;
            var bmp = new Bitmap(imageSize.Width, imageSize.Height);
            using var graphics = Graphics.FromImage(bmp);
            using var brush = new SolidBrush(settings.WordsColor);
            graphics.Clear(settings.BackgroundColor);
            var wordRectanglePairs = layoutRectangles
                .Zip(wordsOrederedByFreq, (layout, word) => (layout, word.Key));
            foreach (var wordRectPair in wordRectanglePairs)
            {
                graphics.DrawString(wordRectPair.Key, settings.Font, brush, wordRectPair.layout);
            }

            return bmp;
        }

        private IReadOnlyCollection<Rectangle> GetLayoutRectangles(IOrderedEnumerable<KeyValuePair<string, int>> wordsOrderedByFreq)
        {
            var startLetterWidth = 50;
            var startLetterHight = 50;
            var mostFreq = wordsOrderedByFreq.First().Value;
            foreach (var word in wordsOrderedByFreq)
            {
                var freqDelta = mostFreq - word.Value;
                var width = (startLetterWidth - freqDelta) * word.Key.Length;
                var height = (startLetterHight - freqDelta);
                var size = new Size(width, height);
                layouter.PutNextRectangle(size);
            }

            return layouter.Rectangles;
        }
    }
}