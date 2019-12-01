using System.Collections.Generic;

namespace TagsCloudContainer.Algorithm
{
    public interface IWordSizeProvider
    {
        IEnumerable<Word> SetWordsSizes(IEnumerable<Word> words);
    }
}