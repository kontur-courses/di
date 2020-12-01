using System.Drawing;

namespace TagsCloud.ImageProcessing.Config
{
    public class ImageConfig : IImageConfig
    {
        public Size ImageSize { get; set; }
        public string Path { get; set; }
    }
}
