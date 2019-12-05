using System.Collections.Generic;

namespace TagsCloudGenerator.FileReaders
{
    public interface IFileReader
    {
        Dictionary<string, int> ReadWords(string path);
    }
}