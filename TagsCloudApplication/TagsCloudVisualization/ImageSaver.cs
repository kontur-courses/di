using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace TagsCloudVisualization
{
    public class ImageSaver
    {
        private readonly ImageFormat format;

        public ImageSaver(ImageFormat format)
        {
            this.format = format;
        }
        
        public void SaveImage(Bitmap image, string filePath)
        {
            if (filePath is null || Directory.Exists(filePath))
                throw new ArgumentException($"Incorrect file path: {filePath}");
            image.Save(filePath, format);
        }
    }
}
