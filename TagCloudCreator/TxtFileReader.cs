using System.IO;

namespace TagCloudCreator
{
    public class TxtFileReader : IFileReader
    {
        public readonly string[] types = {".txt"};

        public string[] Types { get =>types; }

        public string[] ReadAllLinesFromFile(string path)
        {
            return File.ReadAllLines(path);
        }
    }
}