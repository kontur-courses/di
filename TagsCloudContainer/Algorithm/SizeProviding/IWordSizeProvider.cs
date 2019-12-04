using System.Collections.Generic;

namespace TagsCloudContainer.Algorithm.SizeProviding
{
    public interface IWordSizeProvider
    {
        IEnumerable<Word> SetWordsSizes(IEnumerable<Word> words);
    }
}