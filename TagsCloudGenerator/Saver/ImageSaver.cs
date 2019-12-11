using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudGenerator.Saver
{
    public class ImageSaver : IImageSaver
    {
        public void Save(Bitmap bitmap, string output, ImageFormat imageFormat)
        {
            bitmap.Save(output, imageFormat);
        }
    }
}