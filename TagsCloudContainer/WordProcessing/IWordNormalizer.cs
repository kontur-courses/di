using System.Collections.Generic;

namespace TagsCloudContainer.WordProcessing
{
    public interface IWordNormalizer
    {
        IEnumerable<string> NormalizeWords(IEnumerable<string> words);
    }
}