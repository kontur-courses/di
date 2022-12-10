using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace TagCloud.ImageProcessing
{
    public static class ImageSaver
    {
        public static void SaveBitmap (Bitmap bitmap, string fullFileName)
        {
            bitmap.Save(fullFileName);
        }

        public static void SaveBitmapInSolutionSubDirectory(Bitmap bitmap, string subDirectoryName, string fileName)
        {
            var solutionDirectory = GetSolutionDirectory();

            var subDirectory = GetSubDirectory(solutionDirectory, subDirectoryName);

            bitmap.Save(Path.Combine(subDirectory.FullName, fileName));
        }

        public static DirectoryInfo GetSolutionDirectory()
        {
            var currentDirectory = new DirectoryInfo(Environment.CurrentDirectory);

            while (currentDirectory != null && !currentDirectory.GetFiles("*.sln").Any())
            {
                currentDirectory = currentDirectory.Parent;
            }

            return currentDirectory;
        }

        public static DirectoryInfo GetSubDirectory(DirectoryInfo parentDirectory,string subDirectoryName)
        {
            var subDirectoryFullName = Path.Combine(parentDirectory.FullName, subDirectoryName);

            if (!Directory.Exists(subDirectoryFullName))
                return Directory.CreateDirectory(subDirectoryFullName);

            return new DirectoryInfo(subDirectoryFullName);
        }

        public static DirectoryInfo GetSolutionSubDirectory(string subDirectoryName)
        {
            var solutionDirectory = GetSolutionDirectory();

            return GetSubDirectory(solutionDirectory, subDirectoryName);
        }
    }
}
