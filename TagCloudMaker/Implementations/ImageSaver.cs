using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace TagCloud
{
    public class ImageSaver: IImageSaver
    {
        public string SaveImage(Image image, ImageFormat format)
        {
            var pictureName = Path.GetRandomFileName() + ImageFormat.Png;
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, pictureName);

            image.Save(path, format);

            return path;
        }
    }
}