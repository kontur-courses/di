using System.Drawing.Imaging;

namespace TagCloud
{
    public class FileSettings
    {
        public readonly string ImageSavePath;
        public readonly ImageFormat ImageFormat;

        public FileSettings(string imageSavePath, ImageFormat imageFormat)
        {
            ImageSavePath = imageSavePath;
            ImageFormat = imageFormat;
        }

        public static FileSettings GetDefaultSettings() =>
            new FileSettings(HelperMethods.GetProjectDirectory(), ImageFormat.Png);
    }
}
