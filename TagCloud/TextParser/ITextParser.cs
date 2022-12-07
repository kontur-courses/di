using System.Collections.Generic;

namespace TagCloud
{
    internal interface ITextParser
    {
        IReadOnlyList<string> GetWords(string text);
    }
}
