using System.IO;

namespace TagsCloud.Visualization.WordsReaders.FileReaders
{
    public class TxtFileReader : IFileReader
    {
        public string Read(string filename) => File.ReadAllText(filename);
        public bool CanRead(string extension) => extension == "txt";
    }
}