using System.Drawing;

namespace TagsCloudVisualizationTest
{
    internal static class BitmapExtension
    {
        public static byte[] ToBytes(this Bitmap bmp)
        {
            var converter = new ImageConverter();
            return (byte[])converter.ConvertTo(bmp, typeof(byte[]));
        }
    }
}