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

        public static string GetHunspellDictionariesPath(string name)
        {
            return Path.Combine(GetFolderPath("HunspellDictionaries"), name);
        }

        private static string GetFolderPath(string folderName)
        {
            var projectPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\")) ;
            return Path.Combine(projectPath, folderName);
        }
    }
}