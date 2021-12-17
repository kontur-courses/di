using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud2
{
    public interface IImageFormatter
    {
        ImageCodecInfo Codec { get; }
        EncoderParameters Parameters { get; }
    }
}
