using System.Collections.Generic;

namespace TagsCloudContainer.FileReader
{
    public interface IFileReader
    {
        public TextFileFormat Format { get; }
        public IEnumerable<string> ReadWords(string path);
    }
}