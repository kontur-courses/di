using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud2.Image
{
    public class PngImageFormatter : IImageFormatter
    {
        public ImageCodecInfo GetCodec()
        {
            return ImageFormatterHelper.GetEncoderInfo("image/png");
        }

        public EncoderParameters GetParameters()
        {
            return null;
        }
    }
}
