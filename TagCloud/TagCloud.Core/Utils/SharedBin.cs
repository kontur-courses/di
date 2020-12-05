using System;
using System.IO;

namespace TagCloud.Core.Utils
{
    public static class SharedBin
    {
        private const string RootFolder = "TagCloud";

        private static readonly string binFolderPath =
            Path.Combine(GoParentUntil(AppDomain.CurrentDomain.BaseDirectory, p => p.Name == RootFolder), "bin");

        public static string File(string fileName) => Path.Combine(binFolderPath, fileName);

        private static string GoParentUntil(string path, Func<DirectoryInfo, bool> predicate)
        {
            var dir = new DirectoryInfo(path);
            while (!predicate.Invoke(dir))
                dir = dir!.Parent;
            return dir!.FullName;
        }
    }
}