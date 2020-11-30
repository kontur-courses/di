using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace TagsCloud.BitmapSaver
{
    class BitmapSaver : IBitmapSaver
    {
        public void Save(Bitmap bitmap, ImageFormat format, string directoryPath = ".")
        {
            if (!Directory.Exists(directoryPath))
                throw new ArgumentException("This directory does not exist.");

            var filePath = Path.GetFullPath(CreateFilePath(directoryPath, format));

            bitmap.Save(filePath, ImageFormat.Png);
        }

        private static string CreateFilePath(string directoryPath, ImageFormat format)
        {
            var fileName = DateTime.Now.ToString("yyyyMMddhhmmss");

            return $@"{directoryPath}\{fileName}.{format.ToString().ToLower()}";
        }
    }
}
