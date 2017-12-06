using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using TagCloud.Interfaces;

namespace TagCloud.Implementations
{
    public class ImageSaver: IImageSaver
    {
        public string SaveImage(Image image, ImageFormat format)
        {
            var picturePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.GetRandomFileName());

            image.Save($"{picturePath}.{format}", format);

            return $"{picturePath}.{format}";
        }
    }
}