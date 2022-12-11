using System.Collections.Generic;

namespace TagCloud.TextParsing
{
    public interface ITextParser
    {
        IReadOnlyList<string> GetWords(string text);
    }
}
