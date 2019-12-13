using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudContainer.Savers
{
    public class DefaultImageSaver : IImageSaver
    {
        private static readonly IDictionary<string, ImageFormat> Formats;

        public IEnumerable<string> Extensions => Formats.Keys;

        static DefaultImageSaver()
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

        public void Save(string path, string extension, Image image)
        {
            image.Save(path, Formats[extension]);
        }
    }
}