using System.Drawing;
using System.Drawing.Imaging;
using TagCloud.Interfaces;

namespace TagCloud
{
    public class ImageSaver : IImageSaver
    {
        public void Save(Image image, string path)
        {
            image.Save(path, ImageFormat.Png);
        }
    }
}