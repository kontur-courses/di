using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TagsCloudContainer.FileReaders
{
    public class TxtReader : IFileReader
    {
        public IEnumerable<string> ReadWordsFromFile(string path)
        {
            return File.ReadAllLines(path).Where(s => s != string.Empty);
        }
    }
}