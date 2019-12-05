using System;
using System.Drawing.Imaging;
using System.IO;

namespace TagsCloudVisualization.PathFinders
{
    public static class PathFinder
    {
        public static string GetTextsPath(string fileName)
        {
            return Path.Combine(GetFolderPath("texts"), fileName);
        }
        
        public static string GetImagesPath(string name, ImageFormat imageFormat)
        {
            return Path.Combine(GetFolderPath("Images"), $"{name}.{imageFormat}");
        }

        private static string GetFolderPath(string folderName)
        {
            var projectPath =
                Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Environment.CurrentDirectory)));
            return Path.Combine(projectPath, folderName);
        }
    }
}