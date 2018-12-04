using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.ResultRenderer
{
    public interface IResultRenderer
    {
        Image Generate(IEnumerable<Word> words);
        SizeF GetWordSize(Word word);
    }
}