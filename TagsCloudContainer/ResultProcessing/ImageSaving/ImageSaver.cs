using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudContainer.ResultProcessing.ImageSaving
{
    public class ImageSaver : IImageSaver
    {
        private readonly string filePath;
        private readonly ImageFormat imageFormat;

        public ImageSaver(string filePath, ImageFormat imageFormat)
        {
            this.filePath = filePath;
            this.imageFormat = imageFormat;
        }

        public void SaveBitmap(Bitmap bitmap)
        {
            bitmap.Save(filePath, imageFormat);
        }
    }
}