using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace TagsCloudDrawer.ImageSaveService
{
    public class PngSaveService : IImageSaveService
    {
        public void Save(string filename, Image image)
        {
            image.Save(Path.ChangeExtension(filename, "png"), ImageFormat.Png);
        }
    }
}