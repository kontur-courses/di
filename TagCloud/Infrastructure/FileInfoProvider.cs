using System;
using System.IO;

namespace TagCloud.Infrastructure
{
    public class FileInfoProvider : IFileInfoProvider
    {
        public FileInfo GetFileInfo(string path)
        {
            if (Directory.Exists(path))
                throw new ArgumentException($"{path} should be a file");
            var fileDirectory = Directory.GetParent(path);
            if (!fileDirectory.Exists)
                throw new DirectoryNotFoundException($"Directory {fileDirectory.FullName} doesn't exist");
            if (!File.Exists(path))
                throw new FileNotFoundException($"File {path} doesn't exist");
            return new FileInfo(path);
        }
    }
}