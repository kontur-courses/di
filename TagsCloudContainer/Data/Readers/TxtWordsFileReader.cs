using System.Collections.Generic;
using System.IO;

namespace TagsCloudContainer.Data.Readers
{
    public class TxtWordsFileReader : IFileFormatReader
    {
        public IEnumerable<string> Extensions { get; } = new[] {".txt"};
        public IEnumerable<string> ReadAllWords(string path) => File.ReadAllLines(path);
    }
}