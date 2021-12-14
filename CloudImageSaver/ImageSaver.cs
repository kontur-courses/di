using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace CloudImageSaver
{
    public class ImageSaver
    {
        private readonly string directory;
        private readonly string imageName;
        private readonly ImageFormat format;

        public ImageSaver(string directory, string imageName)
        {
            this.directory = directory;
            if (!Directory.Exists(directory))
                throw new ArgumentException($"No such directory {directory}");
            this.imageName = imageName;
            format = DefineFormat(imageName);
        }

        private static ImageFormat DefineFormat(string imageName)
        {
            var extension = Path.GetExtension(imageName);
            return extension switch
            {
                ".png" => ImageFormat.Png,
                ".jpeg" or ".jpg" => ImageFormat.Jpeg,
                ".bmp" => ImageFormat.Bmp,
                _ => throw new ArgumentException($"Cant save image in this {extension} format.")
            };
        }

        public void Save(Image image)
        {
            var path = Path.Combine(directory, imageName);
            image.Save(path, format);
        }
    }
}