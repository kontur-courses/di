using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Xml.Linq;

namespace TagsCloudContainer
{
    public static class FileSaver
    {
        private static Dictionary<string, ImageFormat> extensions = new Dictionary<string, ImageFormat>()
            {{"jpg", ImageFormat.Jpeg}, {"bmp", ImageFormat.Bmp}, {"png", ImageFormat.Png}};
        public static void Save(Bitmap bitmap, string fileName, string directory, string extension)
        {
            if (!extensions.ContainsKey(extension))
            {
                throw new ArgumentException("Unexpected format");
            }
            bitmap.Save(directory + fileName + "." + extension, extensions[extension]);
        }
    }
}