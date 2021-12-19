using System;
using System.Drawing.Imaging;

namespace TagCloud2.Image
{
    public static class ImageFormatterHelper
    {
        public static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            var encoders = ImageCodecInfo.GetImageEncoders();
            for (int j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }

            throw new ArgumentException("no such codec");
        }
    }
}
