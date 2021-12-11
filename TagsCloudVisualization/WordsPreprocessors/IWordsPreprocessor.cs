using System.Collections.Generic;

namespace TagsCloudVisualization.WordsPreprocessors
{
    public interface IWordsPreprocessor
    {
        IEnumerable<string> Preprocess(IEnumerable<string> words);
    }
}