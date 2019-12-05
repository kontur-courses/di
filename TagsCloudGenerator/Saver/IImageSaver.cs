using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudGenerator.Saver
{
    public interface IImageSaver
    {
        void Save(Bitmap bitmap, string filename, ImageFormat imageFormat);
    }
}