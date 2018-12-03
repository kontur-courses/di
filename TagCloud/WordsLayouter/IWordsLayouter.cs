using System.Collections.Generic;
using System.Drawing;
using TagCloud.Data;

namespace TagCloud.WordsLayouter
{
    public interface IWordsLayouter
    {
        IEnumerable<WordImageInfo> GenerateLayout(IEnumerable<WordInfo> words, FontFamily fontFamily, int fontMultiplier);
    }
}