using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud2.Image
{
    public class BitmapImageFormatter : IImageFormatter
    {
        public ImageCodecInfo Codec => ImageFormatterHelper.GetEncoderInfo("image/bmp");

        public EncoderParameters Parameters => null;
    }
}
