using System.Drawing;

namespace TagsCloud.ImageProcessing.Config
{
    public interface IImageConfig
    {
        Size ImageSize { get; set; }
        string Path { get; set; }
    }
}
