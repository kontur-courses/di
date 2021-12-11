using System.Collections.Generic;

namespace TagsCloudVisualization.WordsPreprocessors.Filters
{
    public interface IWordsFilter
    {
        IEnumerable<string> Filter(IEnumerable<string> words);
    }
}