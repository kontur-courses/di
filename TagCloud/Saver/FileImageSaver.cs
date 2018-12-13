using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace TagCloud.Saver
{
    public class FileImageSaver : IImageSaver
    {
        public static readonly Dictionary<string, ImageFormat> Formats = new Dictionary<string, ImageFormat>
        {
            ["png"] = ImageFormat.Png,
            ["jpg"] = ImageFormat.Jpeg,
            ["bmp"] = ImageFormat.Bmp
        };

        public void Save(Image image, string fileName)
        {
            image.Save(fileName, Formats[fileName.Split('.')[1]]);
        }
    }
}