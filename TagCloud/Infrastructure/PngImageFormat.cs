using System.Drawing;

namespace TagCloud.Infrastructure
{
    public class PngImageFormat : IImageFormat
    {
        public void SaveImage(Bitmap bitmap, string filePath)
        {
            bitmap.Save($"{filePath}.png");
        }
    }
}
