using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloud.ImageSavers
{
    public class ImageJpgSaver : IImageSaver
    {
        public string[] FileExtensions => new string[] { ".jpg", ".jpeg" };

        public void Save(Image image, string filename)
        {
            image.Save(filename, ImageFormat.Jpeg);
        }
    }
}
