using System.Collections.Generic;

namespace TagsCloudContainer.Preprocessor
{
    public interface IPreprocessor
    {
        IEnumerable<string> PrepareWords(IEnumerable<string> words);
    }
}