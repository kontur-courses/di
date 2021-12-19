using System.Drawing.Imaging;

namespace TagCloud2
{
    public interface IImageFormatter
    {
        ImageCodecInfo Codec { get; }
        EncoderParameters Parameters { get; }
    }
}
