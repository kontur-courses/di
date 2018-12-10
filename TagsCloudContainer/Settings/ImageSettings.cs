using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudContainer.Settings
{
    public class ImageSettings
    {
        public Size ImageSize;
        public ImageFormat Format = ImageFormat.Png;

        public ImageSettings(Size imageSize)
        {
            ImageSize = imageSize;
        }
    }
}