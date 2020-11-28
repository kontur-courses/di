using System;
using System.Collections.Generic;
using System.IO;

namespace TagCloud.Core.FileReaders
{
    public class TxtReader : IFileReader
    {
        public IEnumerable<string> ReadAllWords(string filePath)
        {
            return File.ReadAllText(filePath).Split();
        }

        public bool IsValidExtension(string extension)
        {
            return extension.Equals(".txt", StringComparison.OrdinalIgnoreCase);
        }
    }
}