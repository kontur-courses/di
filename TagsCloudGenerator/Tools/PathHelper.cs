using System;
using System.IO;

namespace TagsCloudGenerator.Tools
{
    public static class PathHelper
    {
        public static string GetFileExtension(string path)
        {
            var extension = Path.GetExtension(path);

            if (extension == null)
                throw new ArgumentException("invalid path");

            return extension.Substring(1);
        }
    }
}