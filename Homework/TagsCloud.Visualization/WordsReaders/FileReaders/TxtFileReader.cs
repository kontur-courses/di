using System.IO;

namespace TagsCloud.Visualization.WordsReaders.FileReaders
{
    public class TxtFileReader : IFileReader
    {
        public string Extension => ".txt";
        public string Read(string filename) => File.ReadAllText(filename);
    }
}