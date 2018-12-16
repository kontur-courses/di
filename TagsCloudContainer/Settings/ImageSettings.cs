using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudContainer.Settings
{
    public class ImageSettings: IImageSettings
    {
        public Size ImageSize { get; set; }
        public ImageFormat Format { get; set; }
        public int BaseFontSize { get; set; }

        public ImageSettings()
        {
            ImageSize = new Size(1024, 1280);
            Format = ImageFormat.Png;
            BaseFontSize = 30;
        }
    }
}