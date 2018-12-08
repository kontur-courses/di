using System.Collections.Generic;

namespace TagCloud
{
    internal interface IWordsPreparer
    {
        List<WordInfo> PrepareWords(List<WordInfo> words);
    }
}
