using System.Collections.Generic;

namespace TagsCloudContainer
{
    public interface IPreprocessor
    {
        IEnumerable<string> GetValidWords(IEnumerable<string> words);
    }
}
