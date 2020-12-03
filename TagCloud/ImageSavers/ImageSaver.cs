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
        private string ext;

        public ImageSaver(SaverSettings settings)
        {
            path = settings.Path ?? Directory.GetCurrentDirectory();
            fileName = settings.FileName;
            ext = settings.Extention;
        }

        public void Save(Bitmap bitmap)
        {
            var fullPath = $"{path}/{fileName}.{ext}";
            bitmap.Save(fullPath);
            Console.WriteLine($"Tag cloud visualization saved to file {fullPath}");
        }
    }
}