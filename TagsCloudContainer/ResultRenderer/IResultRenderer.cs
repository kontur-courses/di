using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.ResultRenderer
{
    public interface IResultRenderer : IDisposable
    {
        Image Generate(IEnumerable<Word> words);
        SizeF GetWordSize(Word word);
    }
}