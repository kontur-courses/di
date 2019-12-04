using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization;

namespace TagsCloudContainer.Algorithm.Layouting
{
    public class CircularLayouter : ILayouter
    {
        public IEnumerable<(string, Rectangle)> GetWordsRectangles(IEnumerable<Word> words, Size pictureSize)
        {
            var circularCloudLayouter =
                new CircularCloudLayouter(new Point(pictureSize.Width / 2, pictureSize.Height / 2), pictureSize);
            return words
                .Select(w => (w.Value, circularCloudLayouter.PutNextRectangle(w.Size)));
        }
    }
}