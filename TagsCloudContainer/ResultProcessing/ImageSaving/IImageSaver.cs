using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudContainer.ResultProcessing.ImageSaving
{
    public interface IImageSaver
    {
        void SaveBitmap(Bitmap bitmap, string filePath, ImageFormat imageFormat);
    }
}