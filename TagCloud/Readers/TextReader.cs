using System.IO;

namespace TagCloud.Readers
{
    public class TextReader : IFileReader
    {
        public string[] ReadFile(string filename)
        {
            return File.ReadAllLines(filename);
        }
    }
}
