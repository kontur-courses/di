using System.Collections.Generic;

namespace TagsCloudContainer.Preprocessing
{
    public interface IWordsPreprocessor
    {
        IEnumerable<string> Preprocess(IEnumerable<string> words);
    }
}