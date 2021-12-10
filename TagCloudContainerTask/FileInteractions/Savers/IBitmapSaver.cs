using System.Drawing;
using System.Drawing.Imaging;

namespace FileInteractions.Savers
{
    public interface IBitmapSaver
    {
        void SaveBitmapTo(
            Bitmap bitmap,
            string directory, string file, ImageFormat imageFormat,
            bool openAfterSave = false);
    }
}