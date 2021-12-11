using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace TagsCloudContainer.ImageSaver
{
    public class ImageSaver : IImageSaver
    {
        public void Save(Bitmap bitmap, string imagePath)
        {
            var imageFormat = GetImageFormat(Path.GetExtension(imagePath));
            bitmap.Save($"{imagePath}", imageFormat);
        }

        private static ImageFormat GetImageFormat(string format)
        {
            return format switch
            {
                ".png" => ImageFormat.Png,
                ".bmp" => ImageFormat.Bmp,
                ".jpg" => ImageFormat.Jpeg,
                ".gif" => ImageFormat.Gif,
                _ => throw new ArgumentException($"{format} format is not supported")
            };
        }
    }
}