using System.IO;

namespace TagCloudCreator
{
    public class TxtFileReader : IFileReader
    {
        public string[] Types { get; } = {".txt"};

        public string[] ReadAllLinesFromFile(string path)
        {
            return File.ReadAllLines(path);
        }
    }
}