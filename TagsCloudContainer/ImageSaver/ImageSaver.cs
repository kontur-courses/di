using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer.ImageSaver
{
    public class ImageSaver : IImageSaver
    {
        private readonly IAppSettings appSettings;
        private readonly ImageFormat imageFormat;

        public ImageSaver(IAppSettings appSettings)
        {
            this.appSettings = appSettings;
            imageFormat = GetImageFormat(Path.GetExtension(appSettings.ImagePath));
        }

        public void Save(Bitmap bitmap)
        {
            bitmap.Save($"{appSettings.ImagePath}", imageFormat);
        }

        private static ImageFormat GetImageFormat(string format)
        {
            return format switch
            {
                ".png" => ImageFormat.Png,
                ".bmp" => ImageFormat.Bmp,
                ".jpg" => ImageFormat.Jpeg,
                ".gif" => ImageFormat.Gif,
                _ => throw new ArgumentException($"{format} format is not supported")
            };
        }
    }
}