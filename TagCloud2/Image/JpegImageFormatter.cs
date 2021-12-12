using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace TagCloud2.Image
{
    public class JpegImageFormatter : IImageFormatter
    {
        public ImageCodecInfo GetCodec()
        {
            return ImageFormatterHelper.GetEncoderInfo("image/jpeg");
        }

        public EncoderParameters GetParameters()
        {
            var myEncoder = System.Drawing.Imaging.Encoder.Quality;
            var myEncoderParameters = new EncoderParameters(1);
            var myEncoderParameter = new EncoderParameter(myEncoder, 100L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            return myEncoderParameters;
        }
    }
}
