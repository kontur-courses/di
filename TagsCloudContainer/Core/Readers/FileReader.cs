using System.Collections.Generic;
using System.IO;

namespace TagsCloudContainer.Core.Readers
{
    class FileReader : IReader
    {
        public IEnumerable<string> ReadWords(string path) => File.ReadAllText(path).Split();
    }
}
