using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudContainer.Core.ImageSavers
{
    class ImageSaver : IImageSaver
    {
        private readonly Dictionary<string, ImageFormat> formats;

        public ImageSaver()
        {
            formats = new Dictionary<string, ImageFormat>
            {
                ["jpeg"] = ImageFormat.Jpeg,
                ["jpg"] = ImageFormat.Jpeg,
                ["png"] = ImageFormat.Png,
                ["gif"] = ImageFormat.Gif,
                ["bmp"] = ImageFormat.Bmp,
                ["tif"] = ImageFormat.Tiff
            };
        }

        public void Save(string pathImage, Bitmap bitmap, string format)
        {
            if (!formats.ContainsKey(format))
                throw new ArgumentException(
                    $"Неподдерживаемый формат\nПоддерживаются {string.Join(", ", formats.Keys)}");
            bitmap.Save(pathImage, formats[format]);
            Console.WriteLine($"Tag cloud visualization saved to file {pathImage}");
        }
    }
}