using System.Collections.Generic;
using TagCloud.Drawing;

namespace TagCloud.PreLayout
{
    public interface IWordLayouter
    {
        List<Word> Layout(IDrawerOptions options, Dictionary<string, int> wordsWithFrequency);
    }
}