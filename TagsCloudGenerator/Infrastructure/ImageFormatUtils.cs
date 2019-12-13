using System.Collections.Generic;
using System.Drawing.Imaging;

namespace TagsCloudGenerator.Infrastructure
{
    public static class ImageFormatUtils
    {
        private static readonly Dictionary<string, ImageFormat> ImageFormats = new Dictionary<string, ImageFormat>()
        {
            ["png"] = ImageFormat.Png,
            ["bmp"] = ImageFormat.Bmp,
            ["tiff"] = ImageFormat.Tiff,
            ["jpeg"] = ImageFormat.Jpeg,
            ["icon"] = ImageFormat.Icon,
            ["exif"] = ImageFormat.Exif,
            ["gif"] = ImageFormat.Gif,
            ["emf"] = ImageFormat.Emf,
            ["wmf"] = ImageFormat.Wmf
        };

        public static ImageFormat GetImageFormatByExtension(string extension)
        {
            return ImageFormats[extension];
        }
    }
}