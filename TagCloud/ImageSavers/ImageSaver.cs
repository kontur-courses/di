using System;
using System.Drawing;
using System.IO;
using TagCloud.Settings;

namespace TagCloud.ImageSavers
{
    public class ImageSaver : IImageSaver
    {
        private string path;
        private string fileName;
        
        public ImageSaver(SaverSettings settings)
        {
            path = settings.Path;
            path ??= Directory.GetCurrentDirectory();
            fileName = settings.FileName;
        }

        public void Save(Bitmap bitmap)
        {
            var fullPath = $"{path}/{fileName}";
            bitmap.Save(fullPath);
            Console.WriteLine($"Tag cloud visualization saved to file {fullPath}");
        }
    }
}