using System.Collections.Generic;

namespace TagsCloud.TextProcessing.TextReaders
{
    public interface IReadersFactory
    {
        public IEnumerable<string> ReadText(string path);
    }
}
