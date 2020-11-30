using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloud.BitmapSaver
{
    interface IBitmapSaver
    {
        public void Save(Bitmap bitmap, ImageFormat format, string directoryPath);
    }
}
