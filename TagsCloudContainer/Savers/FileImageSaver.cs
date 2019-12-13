using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace TagsCloudContainer.Savers
{
    public class FileImageSaver : IImageSaver
    {
        private static readonly IDictionary<string, ImageFormat> Formats;

        static FileImageSaver()
        {
            Formats = new Dictionary<string, ImageFormat>()
            {
                [".png"] = ImageFormat.Png,
                [".bmp"] = ImageFormat.Bmp,
                [".gif"] = ImageFormat.Gif,
                [".jpg"] = ImageFormat.Jpeg,
                [".jpeg"] = ImageFormat.Jpeg,
                [".tif"] = ImageFormat.Tiff,
            };
        }

        public void Save(string path, Image image)
        {
            var extension = Path.GetExtension(path);
            if (extension == null)
                throw new ArgumentException($"Cannot get extension from {path}");
            image.Save(path, Formats[extension]);
        }
    }
}