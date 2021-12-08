using System.Collections.Generic;
using System.Text;

namespace TagCloud.Words.Reading.FromFile
{
    public interface IFileReader
    {
        IEnumerable<string> ReadFromFile(string pathToFile, Encoding encoding);
    }
}