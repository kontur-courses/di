using System.Collections.Generic;

namespace TagsCloudContainer.FileReaders
{
    public interface IFileReader
    {
        HashSet<string> SupportedFormats { get; }
        IEnumerable<string> ReadWordsFromFile(string path);
    }
}