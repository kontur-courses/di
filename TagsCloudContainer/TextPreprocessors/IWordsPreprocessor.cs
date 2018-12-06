using System.Collections.Generic;

namespace TagsCloudContainer.TextPreprocessors
{
    public interface IWordsPreprocessor
    {
        IEnumerable<string> PreprocessWords(string[] words);
    }
}