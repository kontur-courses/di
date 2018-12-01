using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.ResultRenderer
{
    public interface IResultRenderer
    {
        void Generate(IEnumerable<Word> words, string filename);
        SizeF GetWordSize(Word word);
    }
}