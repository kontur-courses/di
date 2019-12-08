using System.Collections.Generic;
using System.Drawing;
using TagCloud.Infrastructure;

namespace TagCloud.WordsPreparation
{
    public interface IWordSizeSetter
    {
        IEnumerable<Word> GetSizedWords(IEnumerable<Word> words, Size pictureSize);
    }
}
