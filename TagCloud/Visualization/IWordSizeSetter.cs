using System.Collections.Generic;
using TagCloud.Infrastructure;

namespace TagCloud.Visualization
{
    public interface IWordSizeSetter
    {
        IEnumerable<Word> GetSizedWords(IEnumerable<Word> words, PictureConfig pictureConfig);
    }
}
