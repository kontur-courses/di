using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TagCloud.Words.Reading.FromFile
{
    public class FileReader : IFileReader
    {
        public IEnumerable<string> ReadFromFile(string pathToFile)
        {
            if (!File.Exists(pathToFile))
                throw new FileNotFoundException($"File {pathToFile} does not exist", pathToFile);

            return File.ReadAllLines(pathToFile, Encoding.UTF8);
        }
    }
}