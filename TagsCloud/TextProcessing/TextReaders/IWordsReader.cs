using System.Collections.Generic;

namespace TagsCloud.TextProcessing.TextReaders
{
    public interface IWordsReader
    {
        public IEnumerable<string> ReadWords(string path);
        public bool CanRead(string path);
    }
}
