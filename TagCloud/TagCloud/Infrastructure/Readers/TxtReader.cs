using System;
using System.IO;
using System.Text;

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
