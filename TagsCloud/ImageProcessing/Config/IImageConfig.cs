using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloud.ImageProcessing.Config
{
    public interface IImageConfig
    {
        public Size ImageSize { get; set; }
        public ImageFormat ImageFormat { get; set; }
        public string Path { get; set; }
    }
}
