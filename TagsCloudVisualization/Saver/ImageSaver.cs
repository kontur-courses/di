using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace TagsCloudVisualization.Saver
{
    public class ImageSaver : IImageSaver
    {
        private readonly string directory;
        private readonly string imageName;

        public ImageSaver(string directory, string imageName)
        {
            this.directory = directory;
            if (!Directory.Exists(directory))
                throw new ArgumentException($"No such directory {directory}");
            this.imageName = imageName;
        }

        public void Save(Image image)
        {
            var pathToDirectory = Path.Combine(Directory.GetCurrentDirectory(), directory);
            var path = Path.Combine(pathToDirectory, imageName);
            image.Save(path, ImageFormat.Bmp);
        }
    }
}