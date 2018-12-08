using System.Collections.Generic;

namespace TagCloud
{
    public interface IWordsCounter
    {
        IReadOnlyDictionary<string, int> CountedWords { get; }
        void UpdateWith(string text);
    }
}