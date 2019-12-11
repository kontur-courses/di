using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;

namespace TagsCloudVisualization
{
    public static class ImageUtilities
    {
        public static ImageFormat GetFormatFromString(string formatName)
        {
            var imageFormatProperty = typeof(ImageFormat)
                .GetProperty(formatName, BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase);
            if (imageFormatProperty is null)
                throw new ArgumentException($"Unknown image format: {formatName}");
            return imageFormatProperty.GetValue(null) as ImageFormat;
        }

        public static void SaveImage(Bitmap image, ImageFormat format, string filePath)
        {
            if (filePath is null || Directory.Exists(filePath))
                throw new ArgumentException($"Incorrect file path: {filePath}");
            image.Save(filePath, format);
        }
    }
}
