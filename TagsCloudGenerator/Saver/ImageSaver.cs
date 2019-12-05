using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudGenerator.Saver
{
    public class ImageSaver : IImageSaver
    {
        public void Save(Bitmap bitmap, string filename, ImageFormat imageFormat)
        {
            bitmap.Save(filename+'.'+imageFormat, imageFormat);
        }
    }
}