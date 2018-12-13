using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace TagsCloudContainer.Visualizer
{
    public static class TagsCloudVisualizerExtensions
    {
        public static byte[] ToByteArray(this Bitmap image, ImageFormat imageFormat)
        {
            using (var memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, imageFormat);

                return memoryStream.ToArray();
            }
        }
    }
}