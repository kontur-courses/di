using System.Collections.Generic;

namespace TagCloudApp
{
    public interface ITextReader
    {
        string Extension { get; }
        bool TryReadWords(string path, out IEnumerable<string> words);
    }
}
