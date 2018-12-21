using System.Collections.Generic;
using Extensions;

namespace TagCloud
{
    public interface IWordsCounter
    {
        IReadOnlyDictionary<string, int> CountedWords { get; }
        Result<None> UpdateWith(string text);
    }
}