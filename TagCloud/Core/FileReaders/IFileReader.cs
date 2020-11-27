using System.Collections.Generic;

namespace TagCloud.Core.FileReaders
{
    public interface IFileReader
    {
        public IEnumerable<string> ReadAllWords(string filePath);
        public bool IsAbleToRead(string filePath);
    }
}