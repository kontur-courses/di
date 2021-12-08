using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TagCloud.Words.Reading.FromFile
{
    public class TxtFileReader : IFileReader
    {
        public IEnumerable<string> ReadFromFile(string pathToFile, Encoding encoding)
        {
            if (!File.Exists(pathToFile))
                throw new FileNotFoundException($"File {pathToFile} does not exist", pathToFile);

            return File.ReadAllLines(pathToFile, encoding);
        }
    }
}