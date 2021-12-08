using System.Collections.Generic;

namespace TagCloud.Words.Reading.FromFile
{
    public interface IFileReader
    {
        IEnumerable<string> ReadFromFile(string pathToFile);
    }
}