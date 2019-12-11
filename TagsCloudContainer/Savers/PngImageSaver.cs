using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudContainer.Savers
{
    public class PngImageSaver : IImageSaver
    {
        public void Save(string path, Image image) => image.Save(path, ImageFormat.Png);
    }
}