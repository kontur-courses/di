using System.Collections.Generic;

namespace TagCloud.Core.FileReaders
{
    public interface IFileReader
    {
        public string Extension { get; }
        public IEnumerable<string> ReadAllWords(string filePath);
    }
}