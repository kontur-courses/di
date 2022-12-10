using System.Collections.Generic;

namespace TagCloud.TextParsing
{
    internal interface ITextParser
    {
        IReadOnlyList<string> GetWords(string text);
    }
}
