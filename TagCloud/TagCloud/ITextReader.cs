using System.Collections.Generic;

namespace TagCloud
{
    internal interface ITextReader
    {
        IEnumerable<string> ReadWords();
    }
}