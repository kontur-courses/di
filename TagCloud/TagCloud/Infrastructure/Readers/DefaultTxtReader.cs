using System;
using System.IO;
using System.Text;

namespace TagCloud
{
    public class DefaultTxtReader : IReader
    {
        public string ReadAllText(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException();
            if (path == null)
                throw new ArgumentNullException();

            return File.ReadAllText(path, Encoding.UTF8);
        }
    }
}
