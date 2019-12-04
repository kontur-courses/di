using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace TagsCloudVisualization
{
    public static class ImageSaver
    {
        private static ImageFormat imageFormat;
        
        public static string  SaveImageToDefaultDirectory(string name, Bitmap image, ImageFormat format)
        {
            imageFormat = format;
            var path = GetImagesPath(name);
            image.Save(path, imageFormat);
            
            return path;
        }

        private static string GetImagesPath(string name)
        {
            var projectPath =
                Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Environment.CurrentDirectory)));
            var textsFolderPath = Path.Combine(projectPath, "Images");
            return Path.Combine(textsFolderPath, $"{name}.{imageFormat}");
            /*return Path.HasExtension(name)
                ? Path.Combine(textsFolderPath, name)
                : Path.Combine(textsFolderPath, $"{name}.png");*/
        }
    }
}