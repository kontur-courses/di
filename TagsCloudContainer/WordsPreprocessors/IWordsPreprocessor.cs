using System.Collections.Generic;

namespace TagsCloudContainer.WordsPreprocessors
{
    public interface IWordsPreprocessor
    {
        IEnumerable<string> Preprocess(IEnumerable<string> words);
    }
}