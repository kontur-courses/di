using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;

namespace TagsCloudVisualization
{
    public class ImageSaver
    {
        private readonly ImageFormat format;

        public ImageSaver(ImageFormat format)
        {
            this.format = format;
        }

        public ImageSaver(string formatName)
        {
            var imageFormatProperty = typeof(ImageFormat)
                .GetProperty(formatName, BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase);
            if (imageFormatProperty is null)
                throw new ArgumentException($"Unknown image format: {formatName}");
            format = imageFormatProperty.GetValue(null) as ImageFormat;
        }
        
        public void SaveImage(Bitmap image, string filePath)
        {
            if (filePath is null || Directory.Exists(filePath))
                throw new ArgumentException($"Incorrect file path: {filePath}");
            image.Save(filePath, format);
        }
    }
}
