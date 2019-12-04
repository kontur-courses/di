using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloud.ImageSavers
{
    public class ImagePngSaver : IImageSaver
    {
        public string[] FileExtensions => new string[] { ".png" };

        public void Save(Image image, string filename)
        {
            image.Save(filename, ImageFormat.Png);
        }
    }
}
