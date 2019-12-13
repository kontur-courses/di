using System.IO;
using System;

namespace TagsCloud.PathValidators
{
    public class PathValidator
    {
        public bool IsValidPath(string path)
        {
            if (path == null)
                throw new ArgumentNullException();
            return File.Exists(path);
        }
    }
}
