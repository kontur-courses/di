using System.Collections.Generic;

namespace TagsCloudContainer.TextPreprocessors
{
    public interface IWordsPreprocessor
    {
        IEnumerable<KeyValuePair<string, int>> PreprocessWords(IEnumerable<string> words,
            IEnumerable<string> wordsToBeExcluded = null);
    }
}