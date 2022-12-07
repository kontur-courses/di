using System.IO;

namespace TagCloud.IReaders
{
    public class TextFileReader : IReader
    {
        private readonly string path;
        public TextFileReader(string path)
        {
            this.path = path;
        }

        public string Read() =>
            File.ReadAllText(path);
    }
}
