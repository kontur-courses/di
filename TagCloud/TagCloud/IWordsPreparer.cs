using System.Collections.Generic;

namespace TagCloud
{
    internal interface IWordsPreparer
    {
        IEnumerable<string> PrepareWords(IEnumerable<string> words);
    }
}
