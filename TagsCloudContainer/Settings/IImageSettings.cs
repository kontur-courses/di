using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudContainer.Settings
{
    public interface IImageSettings
    {
        Size ImageSize { get; set; }
        ImageFormat Format { get; set; }
    }
}