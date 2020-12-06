using System.Drawing.Imaging;
using TagsCloudContainer.Infrastructure.Settings;

namespace TagsCloudContainer.App.Settings
{
    public class ImageFormatSettings : IImageFormatSettingsHolder
    {
        public static readonly ImageFormatSettings Instance = new ImageFormatSettings();

        private ImageFormatSettings()
        {
            Format = ImageFormat.Png;
        }

        public ImageFormat Format { get; set; }
    }
}