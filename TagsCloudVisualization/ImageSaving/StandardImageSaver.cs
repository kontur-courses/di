using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;


namespace TagsCloudVisualization.ImageSaving
{
    public class StandardImageSaver : IImageSaver
    {
        private static readonly Dictionary<string, ImageFormat> ImageFormats = new Dictionary<string, ImageFormat>
        {
            { "png", ImageFormat.Png },
            { "jpg", ImageFormat.Jpeg },
            { "jpeg", ImageFormat.Jpeg },
            { "bmp", ImageFormat.Bmp },
            { "gif", ImageFormat.Gif },
            { "icon", ImageFormat.Icon }
        };

        public void SaveImage(Image image, string extension, string path)
        {
            if (!ImageFormats.ContainsKey(extension))
                throw new ArgumentException("Not supported image type");
            image.Save(path, ImageFormats[extension]);
        }

        public string[] SupportedTypes()
        {
            return ImageFormats.Keys.ToArray();
        }
    }
}
