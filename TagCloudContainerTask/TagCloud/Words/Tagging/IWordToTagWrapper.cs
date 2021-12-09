using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.Words.Tagging
{
    public interface IWordToTagWrapper
    {
        Dictionary<string, Tag> WrapWords(
            Dictionary<string, double> wordsFrequencies, Font font);
    }
}