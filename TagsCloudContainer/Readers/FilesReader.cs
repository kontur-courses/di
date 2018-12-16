using System.Collections.Generic;
using System.IO;

namespace TagsCloudContainer.Readers
{
    public class FilesReader : IReader
    {
        public IEnumerable<string> Read(string fileName)
            => File.ReadLines(fileName);
    }
}