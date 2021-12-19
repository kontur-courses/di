using System.IO;

namespace TagCloud2
{
    public class TxtFileReader : IFileReader
    {
        public string ReadFile(string path)
        {
            return File.ReadAllText(path);
        }
    }
}
