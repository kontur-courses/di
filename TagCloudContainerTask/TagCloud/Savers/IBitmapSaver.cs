using System.Drawing;
using System.Drawing.Imaging;

namespace TagCloud.Savers
{
    public interface IBitmapSaver
    {
        void SaveBitmapTo(
            Bitmap bitmap,
            string directory, string file, ImageFormat imageFormat,
            bool openAfterSave = false);
    }
}