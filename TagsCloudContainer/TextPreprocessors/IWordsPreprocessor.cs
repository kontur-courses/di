using System.Collections.Generic;

namespace TagsCloudContainer.TextPreprocessors
{
    public interface IWordsPreprocessor
    {
        IReadOnlyDictionary<string, int> PreprocessWords(IEnumerable<string> words);
    }
}