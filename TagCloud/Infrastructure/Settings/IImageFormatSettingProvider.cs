using System.Drawing.Imaging;

namespace TagCloud.Infrastructure.Settings
{
    public interface IImageFormatSettingProvider
    {
        public ImageFormat Format { get; set; }
    }
}