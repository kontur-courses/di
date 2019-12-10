using System.Collections.Generic;

namespace TagsCloudVisualization.WordSizing
{
    public interface IWordSizer
    {
        IEnumerable<SizedWord> GetSizedWords(IEnumerable<string> words, int minSize, int step);
    }
}