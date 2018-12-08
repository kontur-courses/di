using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace TagsCloudContainer
{
    public class BitmapSaver : IBitmapSaver
    {
        private readonly Dictionary<string, ImageFormat> formats = new Dictionary<string, ImageFormat>
        {
            ["bmp"] = ImageFormat.Bmp,
            ["emf"] = ImageFormat.Emf,
            ["exif"] = ImageFormat.Exif,
            ["gif"] = ImageFormat.Gif,
            ["ico"] = ImageFormat.Icon,
            ["jpeg"] = ImageFormat.Jpeg,
            ["jpg"] = ImageFormat.Jpeg,
            ["png"] = ImageFormat.Png,
            ["tiff"] = ImageFormat.Tiff,
            ["wmf"] = ImageFormat.Wmf
        };

        public void Save(Bitmap bitmap, string filename)
        {
            var extension = Path.GetExtension(filename) ?? throw new ArgumentException("No extension found.");
            if (!formats.TryGetValue(extension.ToLowerInvariant(), out var imageFormat))
                throw new ArgumentException("Unsupported extension: " + extension);
            bitmap.Save(filename, imageFormat);
        }

        public string[] SupportedExtensions => formats.Select(p => p.Key).ToArray();
    }
}
