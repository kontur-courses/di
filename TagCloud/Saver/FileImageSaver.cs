using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace TagCloud.Saver
{
    public class FileImageSaver : IImageSaver
    {
        private static readonly Dictionary<string, ImageFormat> Formats = new Dictionary<string, ImageFormat>
        {
            ["png"] = ImageFormat.Png,
            ["jpg"] = ImageFormat.Jpeg,
            ["bmp"] = ImageFormat.Bmp
        };

        public static HashSet<string> ImageFormats => new HashSet<string>(Formats.Keys);

        public void Save(Image image, string fileName)
        {
            image.Save(fileName, Formats[fileName.Split('.')[1]]);
        }
    }
}