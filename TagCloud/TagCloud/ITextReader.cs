using System.Collections.Generic;

namespace TagCloud
{
    internal interface ITextReader
    {
        bool TryReadWords(string path, out IEnumerable<string> words);
        string Extension { get; }
    }
}
