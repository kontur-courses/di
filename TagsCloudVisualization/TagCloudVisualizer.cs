using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public class TagCloudVisualizer
    {
        private readonly Options options;

        public TagCloudVisualizer(Options options)
        {
            this.options = options;
        }

        public Bitmap GetVisualization(IEnumerable<string> words, ILayouter layouter, Painter painter)
        {
            var rectangles = GetRectanglesForWords(words, layouter);
            return painter.GetImage(words, rectangles, options);
        }

        private IEnumerable<Rectangle> GetRectanglesForWords(IEnumerable<string> words, ILayouter layouter)
        {
            return words.Select(word => layouter.PutNextRectangle(word.GetSize(options.Font, options.ImageSize)));
        }
    }
}