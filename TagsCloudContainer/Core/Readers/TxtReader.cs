using System.Collections.Generic;
using System.IO;

namespace TagsCloudContainer.Core.Readers
{
    class TxtReader : IReader
    {
        public IEnumerable<string> ReadWords(string path) => File.ReadAllText(path).Split();
        public bool CanRead(string path) => Path.GetExtension(path) == ".txt";
    }
}