using System.Drawing.Imaging;

namespace TagsCloudContainer.Infrastructure.Settings
{
    public interface IImageFormatSettingsHolder
    {
        public ImageFormat Format { get; }
    }
}