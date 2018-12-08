using System.Collections.Generic;

namespace TagsCloudPreprocessor
{
    public interface IWordsValidator
    {
        IEnumerable<string> GetValidWords(IEnumerable<string> words);
    }
}
