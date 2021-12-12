using System.Collections.Generic;
using TagCloud.Drawing;
using TagCloud.TextProcessing;

namespace TagCloud.Layout
{
    public interface IWordLayouter
    {
        List<Word> Layout(IDrawerOptions options, Dictionary<string, int> wordsWithFrequency);
    }
}