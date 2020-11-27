using System.Collections.Generic;

namespace TagsCloud.TextProcessing
{
    public interface ITextReader
    {
        public IEnumerable<string> ReadWords(string path);
        public bool CanRead(string path);
    }
}
