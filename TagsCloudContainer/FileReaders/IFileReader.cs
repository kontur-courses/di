using System.Collections.Generic;

namespace TagsCloudContainer.FileReaders
{
    public interface IFileReader
    {
        IEnumerable<string> ReadWordsFromFile(string path);
    }
}