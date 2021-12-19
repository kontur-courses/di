using System.Drawing.Imaging;

namespace TagCloud2.Image
{
    public class BitmapImageFormatter : IImageFormatter
    {
        public ImageCodecInfo Codec => ImageFormatterHelper.GetEncoderInfo("image/bmp");

        public EncoderParameters Parameters => null;
    }
}
