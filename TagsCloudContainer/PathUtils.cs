using System;
using System.IO;

namespace TagsCloudContainer
{
    public class PathUtils
    {
        public static string GetExtension(string path)
        {
            return Path.GetExtension(path) ?? throw new ArgumentException($"Cannot get extension from {path}");
        }
    }
}