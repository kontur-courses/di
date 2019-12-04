using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudContainer.ResultProcessing.ImageSaving
{
    public class ImageSaver : IImageSaver
    {
        public void SaveBitmap(Bitmap bitmap, string filePath, ImageFormat imageFormat)
        {
            bitmap.Save(filePath, imageFormat);
        }
    }
}