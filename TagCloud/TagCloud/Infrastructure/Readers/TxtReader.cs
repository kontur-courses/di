using System.IO;

namespace TagCloud
{
    public class TxtReader : IReader
    {
        public string ReadAllText(string pathToFile)
        {
            return File.ReadAllText(pathToFile);
        }
    }
}
