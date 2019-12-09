using System.IO;
using System;

namespace TagsCloud.FileReader
{
    public class PathValidator
    {
        public bool IsValidPath(string path)
        {
            if (path == null)
                throw new ArgumentException();
            return File.Exists(path);
        }
    }
}
