using System.Collections.Generic;

namespace TagsCloudContainer.TextPreprocessors
{
    internal interface IWordsPreprocessor
    {
        IEnumerable<string> PreprocessWords(string[] words);
    }
}