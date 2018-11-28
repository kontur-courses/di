using System.Collections.Generic;

namespace TagsCloudContainer
{
    interface IPreprocessor
    {
        IEnumerable<string> GetValidWords(IEnumerable<string> words);
    }
}
