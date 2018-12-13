using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace TagsCloudContainer.Visualizer
{
    public static class TagsCloudVisualizerExtensions
    {
        public static byte[] ToByteArray(this Bitmap image)
        {
            using (var memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, ImageFormat.Png);

                return memoryStream.ToArray();
            }
        }
    }
}