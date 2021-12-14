using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace TagsCloudVisualization.Common.ImageWriters
{
    public class ImageWriter : IImageWriter
    {
        public void Save(Bitmap bitmap, string path)
        {
            bitmap.Save(path, GetImageFormat(path));
            bitmap.Dispose();
        }

        private static ImageFormat GetImageFormat(string path)
        {
            var extension = Path.GetExtension(path);
            if (string.IsNullOrEmpty(extension))
                return ImageFormat.Bmp;

            switch (extension.ToLower())
            {
                case @".bmp":
                    return ImageFormat.Bmp;

                case @".gif":
                    return ImageFormat.Gif;

                case @".ico":
                    return ImageFormat.Icon;

                case @".jpg":
                case @".jpeg":
                    return ImageFormat.Jpeg;

                case @".png":
                    return ImageFormat.Png;

                case @".tif":
                case @".tiff":
                    return ImageFormat.Tiff;

                case @".wmf":
                    return ImageFormat.Wmf;

                default:
                    throw new ArgumentException("Передан неизвестный формат сохраняемого изображения.");
            }
        }
    }
}