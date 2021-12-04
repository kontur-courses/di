using System.Collections.Generic;

namespace TagsCloudVisualization.WordsPreprocessor
{
    public interface IWordsPreprocessor
    {
        IEnumerable<string> Process(IEnumerable<string> words);
    }
}