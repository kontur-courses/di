using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Algorithm.SizeProviding
{
    public interface IWordSizeProvider
    {
        IEnumerable<Word> SetWordsSizes(IEnumerable<Word> words, Size pictureSize);
    }
}