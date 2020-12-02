using System;
using System.Drawing;
using System.IO;
using TagCloud.Settings;

namespace TagCloud.ImageSavers
{
    public class BmpSaver : IImageSaver
    {
        private string path;
        private string fileName;
        
        public BmpSaver(SaverSettings settings)
        {
            path = settings.Path;
            path ??= Directory.GetCurrentDirectory();
            fileName = settings.FileName;
        }

        public void Save(Bitmap bitmap)
        {
            var fullPath = $"{path}/{fileName}.bmp";
            bitmap.Save(fullPath);
            Console.WriteLine($"Tag cloud visualization saved to file {fullPath}");
        }
    }
}