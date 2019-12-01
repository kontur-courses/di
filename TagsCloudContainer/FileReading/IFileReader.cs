using System.Collections.Generic;

namespace TagsCloudContainer.FileReading
{
    public interface IFileReader
    {
        IEnumerable<string> ReadWords(string textFileName);
    }
}