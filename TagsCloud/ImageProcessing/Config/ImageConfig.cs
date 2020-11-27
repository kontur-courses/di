using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloud.ImageProcessing
{
    class ImageConfig : IImageConfig
    {
        public Size ImageSize { get; set; }
        public ImageFormat ImageFormat { get; set; }
        public string Path { get; set; }
    }
}
