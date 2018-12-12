using System.Collections.Generic;

namespace TagsCloudContainer.Preprocessing
{
    public interface IWordsPreprocessor
    {
        IEnumerable<string> Process(IEnumerable<string> words);
    }
}