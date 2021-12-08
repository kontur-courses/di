using System.Collections.Generic;

namespace TagsCloudContainer.TextReader
{
    public interface IFileReader
    {
        public IEnumerable<string> ReadWords(string path);
    }
}