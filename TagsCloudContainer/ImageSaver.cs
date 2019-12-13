using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace TagsCloudContainer
{
    public class ImageSaver : IImageSaver
    {
        private readonly ImageFormat imageFormat;

        public ImageSaver(ImageFormat imageFormat)
        {
            this.imageFormat = imageFormat;
        }

        public string Save(Bitmap bitmap)
        {
            var path = Directory.GetCurrentDirectory();
            var fullname = $"{path}\\tagCloud.{imageFormat.ToString().ToLower()}";
            bitmap.Save(fullname, imageFormat);
            return fullname;
        }
    }

    public interface IImageSaver
    {
        string Save(Bitmap bitmap);
    }
}