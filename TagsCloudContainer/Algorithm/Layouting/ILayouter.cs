using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Algorithm.Layouting
{
    public interface ILayouter
    {
        IEnumerable<(string, Rectangle)> GetWordsRectangles(IEnumerable<Word> words, Size pictureSize);
    }
}